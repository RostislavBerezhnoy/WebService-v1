using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/ElectricitySupplyPoint")]
    [ApiController]
    public class ElectricitySupplyPointController : ControllerBase
    {
        private readonly AppTestContext _context;

        public ElectricitySupplyPointController(AppTestContext context)
        {
            _context = context;
        }

        // GET: api/ElectricitySupplyPoint
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectricitySupplyPoint>>> GetElectricitySupplyPoint()
        {
            return await _context.ElectricitySupplyPoint.ToListAsync();
        }

        // GET: api/ElectricitySupplyPoint/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectricitySupplyPoint>> GetElectricitySupplyPoint(int id)
        {
            var electricitySupplyPoint = await _context.ElectricitySupplyPoint.FindAsync(id);

            if (electricitySupplyPoint == null)
            {
                return NotFound();
            }

            return electricitySupplyPoint;
        }

        // PUT: api/ElectricitySupplyPoint/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElectricitySupplyPoint(int id, ElectricitySupplyPoint electricitySupplyPoint)
        {
            if (id != electricitySupplyPoint.ID)
            {
                return BadRequest();
            }

            _context.Entry(electricitySupplyPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectricitySupplyPointExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ElectricitySupplyPoint
        [HttpPost]
        public async Task<ActionResult<ElectricitySupplyPoint>> PostElectricitySupplyPoint(ElectricitySupplyPoint electricitySupplyPoint)
        {
            _context.ElectricitySupplyPoint.Add(electricitySupplyPoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetElectricitySupplyPoint), new { id = electricitySupplyPoint.ID }, electricitySupplyPoint);
        }

        // DELETE: api/ElectricitySupplyPoint/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectricitySupplyPoint(int id)
        {
            var electricitySupplyPoint = await _context.ElectricitySupplyPoint.FindAsync(id);
            if (electricitySupplyPoint == null)
            {
                return NotFound();
            }

            _context.ElectricitySupplyPoint.Remove(electricitySupplyPoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElectricitySupplyPointExists(int id)
        {
            return _context.ElectricitySupplyPoint.Any(e => e.ID == id);
        }
    }
}
