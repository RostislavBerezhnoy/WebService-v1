using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/ConsumptionObject")]
    [ApiController]
    public class ConsumptionObjectController : ControllerBase
    {
        private readonly AppTestContext _context;

        public ConsumptionObjectController(AppTestContext context)
        {
            _context = context;
        }

        // GET: api/ConsumptionObject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsumptionObject>>> GetConsumptionObject()
        {
            return await _context.ConsumptionObject.ToListAsync();
        }

        // GET: api/ConsumptionObject/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumptionObject>> GetConsumptionObject(int id)
        {
            var consumptionObject = await _context.ConsumptionObject.FindAsync(id);

            if (consumptionObject == null)
            {
                return NotFound();
            }

            return consumptionObject;
        }

        // PUT: api/ConsumptionObject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumptionObject(int id, ConsumptionObject consumptionObject)
        {
            if (id != consumptionObject.ID)
            {
                return BadRequest();
            }

            _context.Entry(consumptionObject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumptionObjectExists(id))
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

        // POST: api/ConsumptionObject
        [HttpPost]
        public async Task<ActionResult<ConsumptionObject>> PostConsumptionObject(ConsumptionObject consumptionObject)
        {
            _context.ConsumptionObject.Add(consumptionObject);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConsumptionObject), new { id = consumptionObject.ID }, consumptionObject);
        }

        // DELETE: api/ConsumptionObject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumptionObject(int id)
        {
            var consumptionObject = await _context.ConsumptionObject.FindAsync(id);
            if (consumptionObject == null)
            {
                return NotFound();
            }

            _context.ConsumptionObject.Remove(consumptionObject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsumptionObjectExists(int id)
        {
            return _context.ConsumptionObject.Any(e => e.ID == id);
        }
    }
}
