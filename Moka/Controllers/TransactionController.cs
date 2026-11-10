using Microsoft.AspNetCore.Mvc;
using Moka.Models;
using Moka.Data;
using Moka.DTOs;

namespace Moka.Controllers
{
    [Controller]
    [Route("api/[Controller]")]
    public class TransactionsController : ControllerBase
    {

        private readonly MokaDbContext _mokaDbContext;
        private readonly ILogger<TransactionsController> _mokaLogger;
        public TransactionsController(MokaDbContext mokaDbContext, ILogger<TransactionsController> mokaLogger)
        {
            _mokaDbContext = mokaDbContext;
            _mokaLogger = mokaLogger;
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> CreateTransaction(CreateTransactionDto transactionDto)
        {

            _mokaLogger.LogInformation("Creating a new transaction.");

            if (transactionDto == null)
            {
                return BadRequest("Transaction data is required.");
            }

            var transaction = new Transaction
            {
                OriginId = transactionDto.OriginId,
                DestinationId = transactionDto.DestinationId,
                Date = transactionDto.Date,
                MovementType = transactionDto.MovementType,
                Process = transactionDto.Process?.ToUpper(),
                DocumentNumber = transactionDto.DocumentNumber?.ToUpper(),
                Notes = transactionDto.Notes,
                Temporary = transactionDto.Temporary,
                TransactionLines = transactionDto.TransactionLines.Select(lineDto => new TransactionLine
                {
                    MachineId = lineDto.MachineId,
                    Notes = lineDto.Notes
                }).ToList()

            };

            try
            {
                _mokaDbContext.Transactions.Add(transaction);
                await _mokaDbContext.SaveChangesAsync();
                //return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
                return StatusCode(201, "Transaction created successfully.");
            }
            catch (Exception ex)
            {
                _mokaLogger.LogError(ex, "Error creating transaction.");
                return StatusCode(500, "An error occurred while creating the transaction.");
            }
        }
    }
}
