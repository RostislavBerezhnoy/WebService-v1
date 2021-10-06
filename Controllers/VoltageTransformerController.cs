using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/VoltageTransformer")]
    [ApiController]
    public class VoltageTransformerController : ControllerBase
    {
        private readonly AppTestContext _context;

        public VoltageTransformerController(AppTestContext context)
        {
            _context = context;
        }

        // GET: api/VoltageTransformer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoltageTransformer>>> GetVoltageTransformer(int id, int? ConsumptionObjectID, bool WithExpiredDate)
        {
            var items = _context.VoltageTransformer;
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

        // GET: api/VoltageTransformer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoltageTransformer>> GetVoltageTransformer(int id)
        {
            var voltageTransformer = await _context.VoltageTransformer.FindAsync(id);

            if (voltageTransformer == null)
            {
                return NotFound();
            }

            return voltageTransformer;
        }

        // PUT: api/VoltageTransformer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoltageTransformer(int id, VoltageTransformer voltageTransformer)
        {
            if (id != voltageTransformer.ID)
            {
                return BadRequest();
            }

            _context.Entry(voltageTransformer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoltageTransformerExists(id))
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

        // POST: api/VoltageTransformer
        [HttpPost]
        public async Task<ActionResult<VoltageTransformer>> PostVoltageTransformer(VoltageTransformer voltageTransformer)
        {
            _context.VoltageTransformer.Add(voltageTransformer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VoltageTransformerExists(voltageTransformer.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVoltageTransformer", new { id = voltageTransformer.ID }, voltageTransformer);
        }

        // DELETE: api/VoltageTransformer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoltageTransformer(int id)
        {
            var voltageTransformer = await _context.VoltageTransformer.FindAsync(id);
            if (voltageTransformer == null)
            {
                return NotFound();
            }

            _context.VoltageTransformer.Remove(voltageTransformer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoltageTransformerExists(int id)
        {
            return _context.VoltageTransformer.Any(e => e.ID == id);
        }
    }
}
