using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/MeteringDevice")]
    [ApiController]
    public class MeteringDeviceController : ControllerBase
    {
        private readonly AppTestContext _context;

        public MeteringDeviceController(AppTestContext context)
        {
            _context = context;
        }

        // GET: api/MeteringDevice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeteringDevice>>> GetMeteringDevice(int id, int? year)
        {
            if (year != null){
               return await _context.MeteringDevice.Where(c => c.StartDate.Value.Year == year).ToListAsync();
            }
            
            return await _context.MeteringDevice.ToListAsync();
        }

        // GET: api/MeteringDevice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeteringDevice>> GetMeteringDevice(int id)
        {
            var meteringDevice = await _context.MeteringDevice.FindAsync(id);

            if (meteringDevice == null)
            {
                return NotFound();
            }

            return meteringDevice;
        }

        // PUT: api/MeteringDevice/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeteringDevice(int id, MeteringDevice meteringDevice)
        {
            if (id != meteringDevice.ID)
            {
                return BadRequest();
            }

            _context.Entry(meteringDevice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeteringDeviceExists(id))
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

        // POST: api/MeteringDevice
        [HttpPost]
        public async Task<ActionResult<MeteringDevice>> PostMeteringDevice(MeteringDevice meteringDevice)
        {
            _context.MeteringDevice.Add(meteringDevice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMeteringDevice), new { id = meteringDevice.ID }, meteringDevice);
        }

        // DELETE: api/MeteringDevice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeteringDevice(int id)
        {
            var meteringDevice = await _context.MeteringDevice.FindAsync(id);
            if (meteringDevice == null)
            {
                return NotFound();
            }

            _context.MeteringDevice.Remove(meteringDevice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeteringDeviceExists(int id)
        {
            return _context.MeteringDevice.Any(e => e.ID == id);
        }
    }
}
