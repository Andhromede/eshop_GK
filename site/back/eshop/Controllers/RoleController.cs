using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eshop.Models;
using eshop.Helpers;

namespace eshop.Controllers
{
    [ApiController]
    [Route("role")]
    public class RoleController : ControllerBase
    {
        private readonly EshopContext _context;

        public RoleController(EshopContext context)
        {
            _context = context;
        }


        // ************************ GET: all ************************
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }
            return await _context.Roles.ToListAsync();
        }

        // ************************ GET: 1 By Id ************************
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(c => c.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            var roleDto = new RoleDto.model
            {
                Id = role.Id,
                Name = role.Name,
                IsActive = role.IsActive
            };

            return Ok(roleDto);
        }


        // ************************ POST: Create ************************
        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
        }


        // ************************ PUT: 1 by Id ************************
        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> PutRole(int id, RoleDto.model roleDto)

        {
            var roleToUpdate = await _context.Roles.FirstOrDefaultAsync(c => c.Id == id);
            if (roleToUpdate == null)
            {
                return NotFound("Role not found.");
            }

            roleToUpdate.Name = roleDto.Name;
            roleToUpdate.IsActive = roleDto.IsActive;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Role updated successfully." });
        }


        // ************************ DELETE: 1 by Id ************************
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // ************************ FONCTION: Verify if exist ************************
        private bool RoleExists(int id)
        {
          return (_context.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
