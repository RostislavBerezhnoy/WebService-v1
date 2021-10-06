using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/Organisation")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly AppTestContext _context;

        public OrganisationController(AppTestContext context)
        {
            _context = context;
        }

        // GET: api/Organisation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organisation>>> GetOrganisation()
        {
            return await _context.Organisation.ToListAsync();
        }

        // GET: api/Organisation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organisation>> GetOrganisation(int id)
        {
            var organisation = await _context.Organisation.FindAsync(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return organisation;
        }

        // PUT: api/Organisation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganisation(int id, Organisation organisation)
        {
            if (id != organisation.ID)
            {
                return BadRequest();
            }

            _context.Entry(organisation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganisationExists(id))
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

        // POST: api/Organisation
        [HttpPost]
        public async Task<ActionResult<Organisation>> PostOrganisation(Organisation organisation)
        {
            _context.Organisation.Add(organisation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrganisation), new { id = organisation.ID }, organisation);
        }

        // DELETE: api/Organisation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganisation(int id)
        {
            var organisation = await _context.Organisation.FindAsync(id);
            if (organisation == null)
            {
                return NotFound();
            }

            _context.Organisation.Remove(organisation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganisationExists(int id)
        {
            return _context.Organisation.Any(e => e.ID == id);
        }
    }
}
