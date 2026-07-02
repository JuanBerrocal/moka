using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moka.Data;
using Moka.Models;

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
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {

            var stores = await _mokaDbContext.Stores.ToListAsync();

            return Ok(stores);
        }

        [HttpPost]
        public async Task<ActionResult<Store>> CreateStore(Store store)
        {

            _mokaDbContext.Stores.Add(store);

            await _mokaDbContext.SaveChangesAsync();

            return CreatedAtAction( nameof(GetStoreById),
                new {id = store.Id },
                store);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStoreById(int id)
        {
            var store = await _mokaDbContext.Stores.FindAsync(id);

            if (store == null) 
                return NotFound();

            return Ok(store);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStore(int id, Store updatedStore)
        {

            if (id != updatedStore.Id) 
            {
                return BadRequest();
            }

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
