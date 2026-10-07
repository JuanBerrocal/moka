using Microsoft.AspNetCore.Mvc;
using Moka.Data;
using Moka.Models;
using Moka.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Moka.Models.Enums;

namespace Moka.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MachinesController : ControllerBase
    {

        private readonly MokaDbContext _mokaDbContext;
        private ILogger<MachinesController> _mokaLogger;

        public MachinesController(MokaDbContext mokaDbContext, ILogger<MachinesController> logger) {

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
            }).ToListAsync();

            var result = machines;

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MachineDto>> GetMachineById(int id)
        {

            _mokaLogger.LogWarning("Looking for machine {id}", id);
            var machine = await _mokaDbContext.Machines.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (machine == null)
            {
                _mokaLogger.LogInformation("Looking for machine {id}", id);
                return NotFound(new { message = "Machine not found." });
            }

            var result = new MachineDto {
                Id = machine.Id,
                Model = machine.Model,
                Serial = machine.Serial,
                SapCode = machine.SapCode,
                Asset = machine.Asset,
                Type = machine.Type,
                PurchaseDate = machine.PurchaseDate,
                State = machine.State,
                StoreId = machine.StoreId,
                Assignment = machine.Assignment,
                IsExternal = machine.IsExternal,
                Notes = machine.Notes
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MachineDto>> CreateMachine(MachineDto machine) {

            _mokaLogger.LogInformation("Creating a new machine.");

            var newMachine = new Machine {
                Id = machine.Id, 
                Model = machine.Model.Trim().ToUpper(),
                Serial = machine.Serial?.Trim().ToUpper(),
                SapCode = machine.SapCode?.Trim().ToUpper(),
                Asset = machine.Asset?.Trim().ToUpper(),
                Type = machine.Type,
                PurchaseDate = machine.PurchaseDate,
                State = machine.State,
                StoreId = machine.StoreId,
                Assignment = machine.Assignment?.Trim().ToUpper(),
                IsExternal = machine.IsExternal,
                Notes = machine.Notes?.Trim()
            };

            _mokaDbContext.Add(newMachine);

            await _mokaDbContext.SaveChangesAsync();

            var result = new MachineDto { 
                Id = newMachine.Id,
                Model = newMachine.Model,
                Serial = newMachine.Serial,
                SapCode = newMachine.SapCode,
                Asset = newMachine.Asset,
                Type = newMachine.Type,
                PurchaseDate = newMachine.PurchaseDate,
                State = newMachine.State,
                StoreId = newMachine.StoreId,
                Assignment = newMachine.Assignment,
                IsExternal = newMachine.IsExternal,
                Notes = newMachine.Notes?.Trim()
            };

            return CreatedAtAction(nameof(GetMachineById), new { id = newMachine.Id}, result);
        }

    }
}
