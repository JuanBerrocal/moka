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
        public async Task<ActionResult<IEnumerable<StoreDto>>> GetStores()
        {

            _mokaLogger.LogInformation("Getting all stores.");
            

            var stores = await _mokaDbContext.Stores.ToListAsync();
            var result = stores.Select(s => new StoreDto { Id = s.Id, Name = s.Name, SapCode = s.SapCode});

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<StoreDto>> CreateStore(CreateStoreDto storeDto)
        {

            _mokaLogger.LogInformation("Creating a new store.");

            var exists = await _mokaDbContext.Stores.AnyAsync(s => s.SapCode == storeDto.SapCode);

            if (exists)
            {
                return Conflict("SAP code already exists.");
            }

            var store = new Store
            {
                Name = storeDto.Name.Trim().ToUpper(),
                SapCode = storeDto.SapCode?.Trim().ToUpper()
            };

            _mokaDbContext.Stores.Add(store);

            await _mokaDbContext.SaveChangesAsync();

            var result = new StoreDto { Name = store.Name, SapCode = storeDto.SapCode };

            _mokaLogger.LogInformation("New store {Name} created.", storeDto.Name);

            return CreatedAtAction( nameof(GetStoreById),
                new {id = store.Id },
                result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDto>> GetStoreById(int id)
        {

            _mokaLogger.LogInformation("Getting the store {id}.", id);

            var store = await _mokaDbContext.Stores.FindAsync(id);

            if (store == null) {
                _mokaLogger.LogWarning("Store {id} to be deleted not found.", id);
                return NotFound();
            }
                

            var result = new StoreDto { Id = store.Id, Name = store.Name, SapCode = store.SapCode}; 

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStore(int id, UpdateStoreDto updatedStoreDto)
        {
            _mokaLogger.LogInformation("Updating the store {id}.", id);

            var exists = await _mokaDbContext.Stores.AnyAsync(s => (s.Id != id) && (s.SapCode == updatedStoreDto.SapCode));

            if (exists)
            {
                _mokaLogger.LogWarning("Updating store with the SAP code {id} that already exists.", id);
                return Conflict("SAP code already exists.");
            }
            var store = await _mokaDbContext.Stores.FindAsync(id);

            if (store == null)
            {
                _mokaLogger.LogWarning("Store {id} to be updated not found.", id);
                return NotFound();
            }

            store.Name = updatedStoreDto.Name.Trim().ToUpper();
            store.SapCode = updatedStoreDto.SapCode?.Trim().ToUpper();
            
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
