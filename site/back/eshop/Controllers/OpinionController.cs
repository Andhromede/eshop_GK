using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eshop.Models;
using eshop.Helpers;
using System.Data;
/*using System.Drawing;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.Options;*/


namespace eshop.Controllers
{
    [ApiController]
    [Route("opinion")]
    public class OpinionController : ControllerBase
    {
        private readonly EshopContext _context;

        public OpinionController(EshopContext context)
        {
            _context = context;
        }


        // ************************ GET: all ************************
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Opinion>>> GetOpinion()
        {
            var opinions = await _context.Opinions.Include(c => c.Client).ThenInclude(c => c.Role).Include(r => r.Product).ToListAsync();

            if (opinions.Count == 0)
            {
                return NotFound();
            }

            var OpinionDto = opinions.Select(c => new OpinionDto.find
            {
                Id = c.Id,
                Text = c.Text,
                IsValidate = c.IsValidate,
                IsModerate = c.IsModerate,
                Client = c.Client == null ? null : new ClientDto.find
                {
                    Id = c.Client.Id,
                    Email = c.Client.Email,
                    Firstname = c.Client.Firstname,
                    Lastname = c.Client.Lastname,
                    Tel = c.Client.Tel,
                    Adress = c.Client.Adress,
                    Cp = c.Client.Cp,
                    City = c.Client.City,
                    Country = c.Client.Country,
                    IsActive = c.Client.IsActive,
                    Role = c.Client.Role == null ? null : new RoleDto.model
                    {
                        Id = c.Client.Role.Id,
                        Name = c.Client.Role.Name,
                        IsActive = c.Client.Role.IsActive
                    }
                },
                Product = c.Product == null ? null : new ProductDto.model
                {
                    Id = c.Product.Id,
                    Name = c.Product.Name,
                    Description = c.Product.Description,
                    Height = c.Product.Height,
                    Width = c.Product.Width,
                    Length = c.Product.Length,
                    Weight = c.Product.Weight,
                    Capacity = c.Product.Capacity,
                    Price = c.Product.Price,
                    Maker = c.Product.Maker,
                    Color = c.Product.Color,
                    Image = c.Product.Image,
                    IsActive = true
                }

            }).ToList();

            return Ok(OpinionDto);
        }


        // ************************ GET: By Id ************************
        [HttpGet("{id}")]
        public async Task<ActionResult<Opinion>> GetOpinionById(int id)
        {
            var opinion = await _context.Opinions.Include(c => c.Client).ThenInclude(c => c.Role).Include(r => r.Product).FirstOrDefaultAsync(c => c.Id == id);
            if (opinion == null)
            {
                return NotFound();
            }


            var OpinionDto = new OpinionDto.find
            {
                Id = opinion.Id,
                Text = opinion.Text,
                IsValidate = opinion.IsValidate,
                IsModerate = opinion.IsModerate,
                Client = opinion.Client == null ? null : new ClientDto.find
                {
                    Id = opinion.Client.Id,
                    Email = opinion.Client.Email,
                    Firstname = opinion.Client.Firstname,
                    Lastname = opinion.Client.Lastname,
                    Tel = opinion.Client.Tel,
                    Adress = opinion.Client.Adress,
                    Cp = opinion.Client.Cp,
                    City = opinion.Client.City,
                    Country = opinion.Client.Country,
                    IsActive = opinion.Client.IsActive,
                    Role = opinion.Client.Role == null ? null : new RoleDto.model
                    {
                        Id = opinion.Client.Role.Id,
                        Name = opinion.Client.Role.Name,
                        IsActive = opinion.Client.Role.IsActive
                    }
                },
                Product = opinion.Product == null ? null : new ProductDto.model
                {
                    Id = opinion.Product.Id,
                    Name = opinion.Product.Name,
                    Description = opinion.Product.Description,
                    Height = opinion.Product.Height,
                    Width = opinion.Product.Width,
                    Length = opinion.Product.Length,
                    Weight = opinion.Product.Weight,
                    Capacity = opinion.Product.Capacity,
                    Price = opinion.Product.Price,
                    Maker = opinion.Product.Maker,
                    Color = opinion.Product.Color,
                    Image = opinion.Product.Image,
                    IsActive = true
                }
            };

            return Ok(OpinionDto);
        }


        // ************************ POST: 1 ************************
        [HttpPost]
        public async Task<ActionResult<Opinion>> PostOpinion(OpinionDto.create opinionDto)
        {
            var client = await _context.Clients.Include(c => c.Opinions).FirstOrDefaultAsync(c => c.Id == opinionDto.Client);
            if (client == null)
            {
                return BadRequest("Invalid Client ID");
            }

            var product = await _context.Products.Include(p => p.Opinions).FirstOrDefaultAsync(c => c.Id == opinionDto.Product);
            if (product == null)
            {
                return BadRequest("Invalid Product ID");
            }

            Opinion opinion = new Opinion
            {
                Text = opinionDto.Text,
                IsValidate = opinionDto.IsValidate,
                IsModerate = opinionDto.IsModerate,
                Client = client,
                Product = product
            };

            _context.Opinions.Add(opinion);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOpinion), new { id = opinion.Id }, opinionDto);
        }


        // ************************ PUT: 1 by Id ************************
        [HttpPut("{id}")]
        public async Task<ActionResult<Opinion>> PutOpinion(int id, OpinionDto.update opinionDto)
        {
            var opinionToUpdate = await _context.Opinions
                .Include(c => c.Client)
                    .ThenInclude(c => c.Role)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (opinionToUpdate == null)
            {
                return NotFound("User not found.");
            }

            var client = await _context.Clients
                .Include(c => c.Role)
                .FirstOrDefaultAsync(c => c.Id == opinionDto.Client);

            if (client == null)
            {
                return BadRequest("Invalid Client ID");
            }

            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == opinionDto.Product);

            if (product == null)
            {
                return BadRequest("Invalid Product ID");
            }

            opinionToUpdate.Text = opinionDto.Text;
            opinionToUpdate.IsValidate = opinionDto.IsValidate;
            opinionToUpdate.IsModerate = opinionDto.IsModerate;
            opinionToUpdate.Client = client;
            opinionToUpdate.Product = product;

            var role = await _context.Roles.FirstOrDefaultAsync(c => c.Id == client.Role.Id);

            if (role != null)
            {
                client.Role = role;
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Opinion updated successfully." });
        }


        // ************************ DELETE: 1 by Id ************************
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpinion(int id)
        {
            if (_context.Opinions == null)
            {
                return NotFound();
            }
            var opinion = await _context.Opinions.FindAsync(id);
            if (opinion == null)
            {
                return NotFound();
            }

            _context.Opinions.Remove(opinion);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // ************************ FONCTION: Verify if exist ************************
        private bool OpinionExists(int id)
        {
            return (_context.Opinions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
