using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moka.Data;
using Moka.Models;
using Moka.DTOs;

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

            var store = new Store
            {
                Name = storeDto.Name,
                SapCode = storeDto.SapCode
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
        public async Task<ActionResult> UpdateStore(int id, UpdateStoreDto updatedStore)
        {

            var store = await _mokaDbContext.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            store.Name = updatedStore.Name;
            store.SapCode = updatedStore.SapCode;

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
