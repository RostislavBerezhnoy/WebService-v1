using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/SubOrganisation")]
    [ApiController]
    public class SubOrganisationController : ControllerBase
    {
        private readonly AppTestContext _context;

        public SubOrganisationController(AppTestContext context)
        {
            _context = context;
        }

        // GET: api/SubOrganisation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubOrganisation>>> GetSubOrganisation()
        {
            return await _context.SubOrganisation.ToListAsync();
        }

        // GET: api/SubOrganisation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubOrganisation>> GetSubOrganisation(int id)
        {
            var subOrganisation = await _context.SubOrganisation.FindAsync(id);

            if (subOrganisation == null)
            {
                return NotFound();
            }

            return subOrganisation;
        }

        // PUT: api/SubOrganisation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubOrganisation(int id, SubOrganisation subOrganisation)
        {
            if (id != subOrganisation.ID)
            {
                return BadRequest();
            }

            _context.Entry(subOrganisation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubOrganisationExists(id))
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

        // POST: api/SubOrganisation
        [HttpPost]
        public async Task<ActionResult<SubOrganisation>> PostSubOrganisation(SubOrganisation subOrganisation)
        {
            _context.SubOrganisation.Add(subOrganisation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubOrganisation), new { id = subOrganisation.ID }, subOrganisation);
        }

        // DELETE: api/SubOrganisation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubOrganisation(int id)
        {
            var subOrganisation = await _context.SubOrganisation.FindAsync(id);
            if (subOrganisation == null)
            {
                return NotFound();
            }

            _context.SubOrganisation.Remove(subOrganisation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubOrganisationExists(int id)
        {
            return _context.SubOrganisation.Any(e => e.ID == id);
        }
    }
}
