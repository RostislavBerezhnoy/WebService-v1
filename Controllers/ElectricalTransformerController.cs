using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/ElectricalTransformer")]
    [ApiController]
    public class ElectricalTransformerController : ControllerBase
    {
        private readonly AppTestContext _context;

        public ElectricalTransformerController(AppTestContext context)
        {
            _context = context;
        }

        // GET: api/ElectricalTransformer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectricalTransformer>>> GetElectricalTransformer(int id, int? ConsumptionObjectID, bool WithExpiredDate)
        {
            
            var items = _context.ElectricalTransformer;
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

        // GET: api/ElectricalTransformer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectricalTransformer>> GetElectricalTransformer(int id)
        {
            var electricalTransformer = await _context.ElectricalTransformer.FindAsync(id);

            if (electricalTransformer == null)
            {
                return NotFound();
            }

            return electricalTransformer;
        }

        // PUT: api/ElectricalTransformer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElectricalTransformer(int id, ElectricalTransformer electricalTransformer)
        {
            if (id != electricalTransformer.ID)
            {
                return BadRequest();
            }

            _context.Entry(electricalTransformer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectricalTransformerExists(id))
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

        // POST: api/ElectricalTransformer
        [HttpPost]
        public async Task<ActionResult<ElectricalTransformer>> PostElectricalTransformer(ElectricalTransformer electricalTransformer)
        {
            _context.ElectricalTransformer.Add(electricalTransformer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetElectricalTransformer), new { id = electricalTransformer.ID }, electricalTransformer);
        }

        // DELETE: api/ElectricalTransformer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectricalTransformer(int id)
        {
            var electricalTransformer = await _context.ElectricalTransformer.FindAsync(id);
            if (electricalTransformer == null)
            {
                return NotFound();
            }

            _context.ElectricalTransformer.Remove(electricalTransformer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElectricalTransformerExists(int id)
        {
            return _context.ElectricalTransformer.Any(e => e.ID == id);
        }
    }
}
