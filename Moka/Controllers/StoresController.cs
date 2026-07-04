using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moka.Data;
using Moka.Models;
using Moka.DTOs;
using System.Reflection.Emit;

namespace Moka.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StoresController : ControllerBase
    {

        private readonly MokaDbContext _mokaDbContext;
        public StoresController(MokaDbContext mokaDbContext)
        {
            _mokaDbContext = mokaDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreDto>>> GetStores()
        {

            var stores = await _mokaDbContext.Stores.ToListAsync();
            var result = stores.Select(s => new StoreDto { Id = s.Id, Name = s.Name, SapCode = s.SapCode});

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<StoreDto>> CreateStore(CreateStoreDto storeDto)
        {

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

            return CreatedAtAction( nameof(GetStoreById),
                new {id = store.Id },
                result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDto>> GetStoreById(int id)
        {
            var store = await _mokaDbContext.Stores.FindAsync(id);

            if (store == null) 
                return NotFound();

            var result = new StoreDto { Id = store.Id, Name = store.Name, SapCode = store.SapCode}; 

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStore(int id, UpdateStoreDto updatedStoreDto)
        {
            var exists = await _mokaDbContext.Stores.AnyAsync(s => (s.Id != id) && (s.SapCode == updatedStoreDto.SapCode));

            if (exists)
            {
                return Conflict("SAP code already exists.");
            }
            var store = await _mokaDbContext.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            store.Name = updatedStoreDto.Name.Trim().ToUpper();
            store.SapCode = updatedStoreDto.SapCode?.Trim().ToUpper();
            
            await _mokaDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStore(int id)
        {
            var store = await _mokaDbContext.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            _mokaDbContext.Stores.Remove(store);

            await _mokaDbContext.SaveChangesAsync();

            return NoContent();
        }
    }


}
