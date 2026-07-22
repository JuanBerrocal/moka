using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moka.Data;
using Moka.Models;
using Moka.DTOs;
using System.Reflection.Emit;
using System;

namespace Moka.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StoresController : ControllerBase
    {

        private readonly MokaDbContext _mokaDbContext;
        private readonly ILogger<StoresController> _mokaLogger;
        public StoresController(MokaDbContext mokaDbContext, ILogger<StoresController> logger)
        {
            _mokaDbContext = mokaDbContext;
            _mokaLogger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreDto>>> GetStores(
            string? name, string? sapCode, string? tradeName, string? sort, string? direction, int page = 1, int pageSize = 20)
        {

            _mokaLogger.LogInformation("Getting stores.");

            IQueryable<Store> query = _mokaDbContext.Stores.AsNoTracking();

            if(!string.IsNullOrWhiteSpace(name)) 
            {
                name = name.Trim().ToUpper();

                query = query.Where(x => x.Name.Contains(name)); 
            }

            if (!string.IsNullOrWhiteSpace(sapCode))
            {
                sapCode = sapCode.Trim().ToUpper();

                query = query.Where(x => x.SapCode!.StartsWith(sapCode));
            }

            if (!string.IsNullOrWhiteSpace(tradeName))
            {
                tradeName = tradeName.Trim().ToUpper();

                query = query.Where(x => x.TradeName!.Contains(tradeName));
            }

            sort = sort?.Trim().ToLower() ?? "name";
            direction = direction?.Trim().ToLower() ?? "asc";

            switch (sort)
            {
                case "sapcode":
                    if (direction == "desc")
                        query = query.OrderByDescending(x => x.SapCode);
                    else
                        query = query.OrderBy(x => x.SapCode);
                    break;
                case "id":
                    if (direction == "desc")
                        query = query.OrderByDescending (x => x.Id);
                    else
                        query = query.OrderBy(x => x.Id);
                    break;
                case "tradename":
                    if (direction == "desc")
                        query = query.OrderByDescending(x => x.TradeName);
                    else
                        query = query.OrderBy(x => x.TradeName);
                    break;
                case "name":
                default:
                    if (direction == "desc")
                        query = query.OrderByDescending(x => x.Name);
                    else
                        query = query.OrderBy(x => x.Name);
                    break;
                }

            int totalItems = await query.CountAsync();

            // Pagination validations.
            if (page < 1)
            {
                return BadRequest("Page number must be 1 or greater.");
            }
            if (pageSize < 1 || pageSize > 100)
            {
                return BadRequest("Page size must be between 1 and 100.");
            }

            // How many rows to be skipped.
            int skip = (page -1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            var stores = await query.Select(s => new StoreDto { 
                Id = s.Id,
                Name = s.Name,
                SapCode = s.SapCode,
                TradeName = s.TradeName,
                Address = s.Address,
                PostalCode = s.PostalCode,
                City = s.City,
                TaxId = s.TaxId,
                Notes = s.Notes
            }).ToListAsync();

            var result = new PagedResult<StoreDto> { TotalItems = totalItems, Page = page, PageSize = pageSize, Items = stores };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<StoreDto>> CreateStore(CreateStoreDto storeDto)
        {

            _mokaLogger.LogInformation("Creating a new store.");

            var store = new Store
            {
                Name = storeDto.Name.Trim().ToUpper(),
                SapCode = storeDto.SapCode?.Trim().ToUpper(),
                TradeName = storeDto.TradeName?.Trim().ToUpper(),
                Address = storeDto.Address?.Trim().ToUpper(),
                PostalCode = storeDto.PostalCode?.Trim().ToUpper(),
                City = storeDto.City?.Trim().ToUpper(),
                TaxId = storeDto.TaxId?.Trim().ToUpper(),
                Notes = storeDto.Notes?.Trim()
            };
                        
            _mokaDbContext.Stores.Add(store);

            await _mokaDbContext.SaveChangesAsync();

            var result = new StoreDto { 
                Id = store.Id,
                Name = store.Name, 
                SapCode = store.SapCode,
                TradeName = store.TradeName,
                Address = store.Address,
                PostalCode = store.PostalCode,
                City = store.City,
                TaxId = store.TaxId,
                Notes = store.Notes
            };

            _mokaLogger.LogInformation("New store {Name} created.", storeDto.Name);

            return CreatedAtAction( nameof(GetStoreById),
                new {id = store.Id },
                result);
        }

        [HttpPost("import")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ImportStores(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was imported.");
            }

            return Ok($"The file ´{file.FileName}´ was imported.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDto>> GetStoreById(int id)
        {

            _mokaLogger.LogInformation("Getting the store {id}.", id);

            var store = await _mokaDbContext.Stores.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

            if (store == null) {
                _mokaLogger.LogWarning("Store {id} to be deleted not found.", id);
                return NotFound();
            }

            var result = new StoreDto
            {
                Id = store.Id,
                Name = store.Name,
                SapCode = store.SapCode,
                TradeName = store.TradeName,
                Address = store.Address,
                PostalCode = store.PostalCode,
                City = store.City,
                TaxId = store.TaxId,
                Notes = store.Notes
            };

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStore(int id, UpdateStoreDto updatedStoreDto)
        {
            _mokaLogger.LogInformation("Updating the store {id}.", id);

            var store = await _mokaDbContext.Stores.FindAsync(id);

            if (store == null)
            {
                _mokaLogger.LogWarning("Store {id} to be updated not found.", id);
                return NotFound();
            }

            store.Name = updatedStoreDto.Name.Trim().ToUpper();
            store.SapCode = updatedStoreDto.SapCode?.Trim().ToUpper();
            store.TradeName = updatedStoreDto.TradeName?.Trim().ToUpper();
            store.Address = updatedStoreDto.Address?.Trim().ToUpper();
            store.PostalCode = updatedStoreDto.PostalCode?.Trim().ToUpper();
            store.City = updatedStoreDto.City?.Trim().ToUpper();
            store.TaxId = updatedStoreDto.TaxId?.Trim().ToUpper();
            store.Notes = updatedStoreDto.Notes?.Trim();

            await _mokaDbContext.SaveChangesAsync();

            _mokaLogger.LogInformation("Store {Id} updated", id);


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStore(int id)
        {
            _mokaLogger.LogInformation("Deleting the store {id}.", id);

            var store = await _mokaDbContext.Stores.FindAsync(id);

            if (store == null)
            {
                _mokaLogger.LogWarning("Store {id} to be deleted not found.", id);
                return NotFound();
            }

            _mokaDbContext.Stores.Remove(store);

            await _mokaDbContext.SaveChangesAsync();

            _mokaLogger.LogInformation("Store {Id} deleted", id);

            return NoContent();
        }
    }


}
