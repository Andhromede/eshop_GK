using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eshop.Models;
using eshop.Helpers;

namespace eshop.Controllers
{
    [ApiController]
    [Route("Satus")]
    public class StatusController : ControllerBase
    {
        private readonly EshopContext _context;

        public StatusController(EshopContext context)
        {
            _context = context;
        }


        // ************************ GET: all ************************
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
            if (_context.Statuses == null)
            {
                return NotFound();
            }
            return await _context.Statuses.ToListAsync();
        }


        // ************************ GET: 1 By Id ************************
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatusById(int id)
        {
            var status = await _context.Statuses.FirstOrDefaultAsync(c => c.Id == id);
            if (status == null)
            {
                return NotFound();
            }

            var statusDto = new StatusDto.model
            {
                Id = status.Id,
                Name = status.Name,
                IsActive = status.IsActive
            };

            return Ok(statusDto);
        }


        // ************************ POST: Create ************************
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStatus), new { id = status.Id }, status);
        }


        // ************************ PUT: 1 by Id ************************
        [HttpPut("{id}")]
        public async Task<ActionResult<Status>> PutStatus(int id, StatusDto.model statusDto)

        {
            var statusToUpdate = await _context.Statuses.FirstOrDefaultAsync(c => c.Id == id);
            if (statusToUpdate == null)
            {
                return NotFound("Role not found.");
            }

            statusToUpdate.Name = statusDto.Name;
            statusToUpdate.IsActive = statusDto.IsActive;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Status updated successfully." });
        }


        // ************************ DELETE: 1 by Id ************************
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // ************************ FONCTION: Verify if exist ************************
        private bool StatusExists(int id)
        {
            return (_context.Statuses?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
