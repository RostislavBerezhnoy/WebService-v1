using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/ElectricityMeter")]
    [ApiController]
    public class ElectricityMeterController : ControllerBase
    {
        private readonly AppTestContext _context;

        public ElectricityMeterController(AppTestContext context)
        {
            _context = context;
        }

        // GET: api/ElectricityMeter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectricityMeter>>> GetElectricityMeter(int id, int? ConsumptionObjectID, bool WithExpiredDate)
        {

            var items = _context.ElectricityMeter;
            foreach (var item in items)
            {
                item.ElectricityMeteringPointName = _context.ElectricityMeteringPoint.Where(c => c.ID == item.ElectricityMeteringPointID).FirstOrDefault().Name;
            }

            if (ConsumptionObjectID != null && !WithExpiredDate)
            {
                return await items.Where(c => 
                    c.ElectricityMeteringPoint.ConsumptionObject.ID == ConsumptionObjectID).ToListAsync();

            } else if (ConsumptionObjectID == null && WithExpiredDate)
            {
                return await items.Where(c => 
                    c.VerificationDate < DateTime.Now).ToListAsync();

            } else if (ConsumptionObjectID != null && WithExpiredDate)
            {
                return await items.Where(c => 
                    c.ElectricityMeteringPoint.ConsumptionObject.ID == ConsumptionObjectID && c.VerificationDate < DateTime.Now).ToListAsync();
            }

            return await items.ToListAsync();
        }

        // GET: api/ElectricityMeter/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectricityMeter>> GetElectricityMeter(int id)
        {
            var electricityMeter = await _context.ElectricityMeter.FindAsync(id);

            if (electricityMeter == null)
            {
                return NotFound();
            }

            return electricityMeter;
        }

        // PUT: api/ElectricityMeter/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElectricityMeter(int id, ElectricityMeter electricityMeter)
        {
            if (id != electricityMeter.ID)
            {
                return BadRequest();
            }

            _context.Entry(electricityMeter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectricityMeterExists(id))
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

        // POST: api/ElectricityMeter
        [HttpPost]
        public async Task<ActionResult<ElectricityMeter>> PostElectricityMeter(ElectricityMeter electricityMeter)
        {
            _context.ElectricityMeter.Add(electricityMeter);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetElectricityMeter), new { id = electricityMeter.ID }, electricityMeter);
        }

        // DELETE: api/ElectricityMeter/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectricityMeter(int id)
        {
            var electricityMeter = await _context.ElectricityMeter.FindAsync(id);
            if (electricityMeter == null)
            {
                return NotFound();
            }

            _context.ElectricityMeter.Remove(electricityMeter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElectricityMeterExists(int id)
        {
            return _context.ElectricityMeter.Any(e => e.ID == id);
        }
    }
}
