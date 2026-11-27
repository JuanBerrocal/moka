using Microsoft.EntityFrameworkCore;
using Moka.Data;
using Moka.DTOs;
using Moka.Models;
using Moka.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Moka.Services
{
    public class TransactionService
    {
        private readonly MokaDbContext _context;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(
            MokaDbContext context,
            ILogger<TransactionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<CreateTransactionResult> CreateTransactionAsync(
            CreateTransactionDto dto,
            CancellationToken cancellationToken = default)
        {
            // 1. Validate header

            if (dto == null)
                throw new ValidationException("Request body is required.");

            if (dto.OriginId <= 0)
                throw new ValidationException("Invalid origin store.");

            if (dto.DestinationId <= 0)
                throw new ValidationException("Invalid destination store.");

            if (dto.OriginId == dto.DestinationId)
                throw new ValidationException(
                    "Origin and destination must be different.");

            if (!IsValidBusinessDate(dto.Date))
                throw new ValidationException(
                    "Invalid transaction date.");

            if (!Enum.IsDefined(typeof(MovementType), dto.MovementType)
                || dto.MovementType == MovementType.Unknown)
            {
                throw new ValidationException(
                    "Invalid movement type.");
            }

            if (dto.Process?.Trim().Length > 40)
                throw new ValidationException(
                    "Process cannot exceed 40 characters.");

            if (dto.DocumentNumber?.Trim().Length > 20)
                throw new ValidationException(
                    "Document number cannot exceed 20 characters.");

            // 2. Validate transaction lines

            if (dto.TransactionLines == null ||
                dto.TransactionLines.Count == 0)
            {
                throw new ValidationException(
                    "At least one transaction line is required.");
            }

            if (dto.TransactionLines.Any(l => l.MachineId <= 0))
                throw new ValidationException(
                    "Every MachineId must be positive.");

            var machineIds = dto.TransactionLines
                .Select(l => l.MachineId)
                .ToList();

            if (machineIds.Distinct().Count() != machineIds.Count)
                throw new ValidationException(
                    "Every machine cannot appear more than once.");

            // 3. Validate stores

            var storesExist = await _context.Stores
                .Where(s => s.Id == dto.OriginId ||
                            s.Id == dto.DestinationId)
                .CountAsync(cancellationToken);

            if (storesExist != 2)
                throw new ValidationException(
                    "Origin or destination store does not exist.");

            // 4. Retrieve machines

            var machines = await _context.Machines
                .Where(m => machineIds.Contains(m.Id))
                .ToListAsync(cancellationToken);

            if (machines.Count != machineIds.Count)
                throw new ValidationException(
                    "One or more machines do not exist.");

            // 5. Check machine locations and collect warnings

            var warnings = new List<TransactionWarning>();

            foreach (var machine in machines)
            {
                if (machine.StoreId != dto.OriginId)
                {
                    warnings.Add(new TransactionWarning
                    {
                        MachineId = machine.Id,
                        Message =
                            $"Machine {machine.Id} is registered in " +
                            $"store {machine.StoreId}, but the declared " +
                            $"origin is store {dto.OriginId}."
                    });
                }
            }

            // 6. Create transaction header

            var transaction = new Transaction
            {
                OriginId = dto.OriginId,
                DestinationId = dto.DestinationId,
                Date = dto.Date,
                MovementType = dto.MovementType,
                Process = Normalize(dto.Process)?.ToUpperInvariant(),
                DocumentNumber =
                    Normalize(dto.DocumentNumber)?.ToUpperInvariant(),
                Notes = Normalize(dto.Notes),
                Temporary = dto.Temporary
            };

            // 7. Create transaction lines and update machines

            for (int i = 0; i < dto.TransactionLines.Count; i++)
            {
                var lineDto = dto.TransactionLines[i];

                var transactionLine = new TransactionLine
                {
                    LineId = i + 1,
                    MachineId = lineDto.MachineId,
                    Notes = Normalize(lineDto.Notes)
                };

                transaction.TransactionLines.Add(transactionLine);
            }

            foreach (var machine in machines)
            {
                machine.StoreId = dto.DestinationId;
            }

            // 8. Persist everything together

            try
            {
                _context.Transactions.Add(transaction);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(
                    "Transaction {TransactionId} created with {LineCount} lines.",
                    transaction.Id,
                    transaction.TransactionLines.Count);

                return new CreateTransactionResult
                {
                    TransactionId = transaction.Id,
                    Warnings = warnings
                };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Concurrency conflict creating transaction.");

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating transaction.");

                throw;
            }
        }

        private static string? Normalize(string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : value.Trim();
        }

        private static bool IsValidBusinessDate(DateOnly date)
        {
            return date >= new DateOnly(2000, 1, 1)
                && date <= DateOnly.FromDateTime(DateTime.Today);
        }

        public class CreateTransactionResult
        {
            public int TransactionId { get; set; }
            public List<TransactionWarning> Warnings { get; set; } = new();
        }

        public class TransactionWarning
        {
            public int MachineId { get; set; }
            public string Message { get; set; } = string.Empty;
        }
     
        public async Task<PagedResult<TransactionDto>> GetTransactionsAsync(
            TransactionQueryDto filters,
            CancellationToken cancellationToken = default)
        {
            if (filters.FromDate.HasValue &&
                filters.ToDate.HasValue &&
                filters.FromDate > filters.ToDate)
            {
                throw new ValidationException(
                    "FromDate cannot be later than ToDate.");
            }

            if (filters.OriginId.HasValue && filters.OriginId <= 0)
                throw new ValidationException("Invalid origin store ID.");

            if (filters.DestinationId.HasValue && filters.DestinationId <= 0)
                throw new ValidationException("Invalid destination store ID.");

            if (filters.MovementType.HasValue &&
                (!Enum.IsDefined(typeof(MovementType), filters.MovementType.Value) ||
                 filters.MovementType == MovementType.Unknown))
            {
                throw new ValidationException("Invalid movement type.");
            }

            var query = _context.Transactions.AsNoTracking().AsQueryable();

            // 1. Apply filters

            if (filters.OriginId.HasValue)
                query = query.Where(t => t.OriginId == filters.OriginId.Value);

            if (filters.DestinationId.HasValue)
                query = query.Where(t => t.DestinationId == filters.DestinationId.Value);

            if (filters.MovementType.HasValue)
                query = query.Where(t => t.MovementType == filters.MovementType.Value);

            if (filters.FromDate.HasValue)
                query = query.Where(t => t.Date >= filters.FromDate.Value);

            if (filters.ToDate.HasValue)
                query = query.Where(t => t.Date <= filters.ToDate.Value);

            if (filters.Temporary.HasValue)
                query = query.Where(t => t.Temporary == filters.Temporary.Value);

            // 2. Count matching transactions before pagination

            var totalCount = await query.CountAsync(cancellationToken);

            // 3. Apply sorting

            var sortBy = filters.SortBy?.Trim().ToLowerInvariant();

            query = sortBy switch
            {
                "id" => filters.SortDescending
                    ? query.OrderByDescending(t => t.Id)
                    : query.OrderBy(t => t.Id),

                "date" => filters.SortDescending
                    ? query.OrderByDescending(t => t.Date)
                    : query.OrderBy(t => t.Date),

                "documentnumber" => filters.SortDescending
                    ? query.OrderByDescending(t => t.DocumentNumber)
                    : query.OrderBy(t => t.DocumentNumber),

                "movementtype" => filters.SortDescending
                    ? query.OrderByDescending(t => t.MovementType)
                    : query.OrderBy(t => t.MovementType),

                "originid" => filters.SortDescending
                    ? query.OrderByDescending(t => t.OriginId)
                    : query.OrderBy(t => t.OriginId),

                "destinationid" => filters.SortDescending
                    ? query.OrderByDescending(t => t.DestinationId)
                    : query.OrderBy(t => t.DestinationId),

                _ => throw new ValidationException(
                    "Invalid sort field. Allowed: id, date, documentNumber, movementType, originId, destinationId.")
            };

            // 4. Add a stable secondary sort

            query = query.ThenBy(t => t.Id);

            // 5. Apply pagination

            var skip = (long)(filters.Page - 1) * filters.PageSize;

            var transactions = skip > int.MaxValue
                ? new List<TransactionDto>()
                : await query
                    .Skip((int)skip)
                    .Take(filters.PageSize)
                    .Select(t => new TransactionDto
                    {
                        Id = t.Id,
                        OriginId = t.OriginId,
                        DestinationId = t.DestinationId,
                        Date = t.Date,
                        MovementType = t.MovementType,
                        Process = t.Process,
                        DocumentNumber = t.DocumentNumber,
                        Notes = t.Notes,
                        Temporary = t.Temporary,

                        TransactionLines = t.TransactionLines
                            .OrderBy(l => l.LineId)
                            .Select(l => new TransactionLineDto
                            {
                                LineId = l.LineId,
                                MachineId = l.MachineId,
                                Notes = l.Notes
                            })
                            .ToList()
                    })
                    .ToListAsync(cancellationToken);

            // 6. Return paginated result

            return new PagedResult<TransactionDto>
            {
                TotalItems = totalCount,
                Page = filters.Page,
                PageSize = filters.PageSize,
                Items = transactions
            };
        }


    }
}

