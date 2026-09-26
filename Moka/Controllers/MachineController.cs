using Microsoft.AspNetCore.Mvc;
using Moka.Data;
using Moka.Models;
using Moka.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Moka.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MachineController : ControllerBase
    {

        private readonly MokaDbContext _mokaDbContext;
        private ILogger<MachineController> _mokaLogger;

        public MachineController(MokaDbContext mokaDbContext, ILogger<MachineController> logger) { 

            _mokaDbContext = mokaDbContext;
            _mokaLogger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineDto>>> GetMachines() {
            IQueryable<Machine> query = _mokaDbContext.Machines.AsNoTracking();

            var machines = await query.Select(m => new MachineDto { 
                Id = m.Id,
                Model = m.Model,
                Serial = m.Serial,
                SapCode = m.SapCode,
                Asset = m.Asset,
                Type = m.Type,
                PurchaseDate = m.PurchaseDate,
                State = m.State,
                StoreId = m.StoreId,
                Assignment = m.Assignment,
                IsExternal = m.IsExternal,
                Notes = m.Notes
                } ).ToListAsync();

            var result = machines;

            return Ok(result);
            
        }

    }
}
