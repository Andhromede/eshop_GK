
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eshop.Models;
using eshop.Helpers;
using System.Data;
/*using System.Diagnostics.Metrics;*/


namespace eshop.Controllers
{
    [ApiController]
    [Route("client")]
    public class ClientController : ControllerBase
    {
        private readonly EshopContext _context;

        public ClientController(EshopContext context)
        {
            _context = context;
        }


        // ************************ GET: all ************************
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient()
        {
            var clients = await _context.Clients.Include(c => c.Role).ToListAsync();

            if (clients.Count == 0)
            {
                return NotFound();
            }

            var clientDTOs = clients.Select(c => new ClientDto.find
            {
                Id = c.Id,
                Email = c.Email,
                Firstname = c.Firstname,
                Lastname = c.Lastname,
                Tel = c.Tel,
                Adress = c.Adress,
                Cp = c.Cp,
                City = c.City,
                Country = c.Country,
                IsActive = c.IsActive,
                Role = new RoleDto.model
                {
                    Id = c.Role.Id,
                    Name = c.Role.Name,
                    IsActive =c.Role.IsActive
                }
            }).ToList();

            return Ok(clientDTOs);
        }


        // ************************ POST: 1 ************************
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(ClientDto.create clientDto)
        {
           var role = await _context.Roles.Include(r => r.Clients).FirstOrDefaultAsync(c => c.Id == 1);
            if (role == null)
            {
                return BadRequest("Invalid Role ID");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(clientDto.Password); 

            /* !!! ATTENTION : ne pas indiquer de role lors de la requete !!! */
            Client client = new Client
            {
                Email = clientDto.Email,
                Password = hashedPassword,
                Firstname = clientDto.Firstname,
                Lastname = clientDto.Lastname,
                Tel = clientDto.Tel,
                Adress = clientDto.Adress,
                Cp = clientDto.Cp,
                City = clientDto.City,
                Country = clientDto.Country,
                IsActive = clientDto.IsActive,
                Role = role
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, clientDto);
        }


        // ************************ GET: By Id ************************
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var client = await _context.Clients.Include(c => c.Role).FirstOrDefaultAsync(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            var clientDto = new ClientDto.find
            {
                Id = client.Id,
                Email = client.Email,
                Firstname = client.Firstname,
                Lastname = client.Lastname,
                Tel = client.Tel,
                Adress = client.Adress,
                Cp = client.Cp,
                City = client.City,
                Country = client.Country,
                IsActive = client.IsActive,
                Role = new RoleDto.model
                {
                    Id = client.Role.Id,
                    Name = client.Role.Name,
                    IsActive = client.Role.IsActive
                }
            };

            return Ok(clientDto);
        }


        // ************************ PUT: 1 by Id ************************
        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> PutClient(int id, ClientDto.update clientDto)
        {
            var clientToUpdate = await _context.Clients.Include(c => c.Role).FirstOrDefaultAsync(c => c.Id == id);
            if (clientToUpdate == null)
            {
                return NotFound("User not found.");
            }

            var role = await _context.Roles.FirstOrDefaultAsync(c => c.Id == clientDto.Role);
            if (role == null)
            {
                return BadRequest("Invalid Role ID");
            }

            // Update properties only if the new values are not null
            clientToUpdate.Email = clientDto.Email ?? clientToUpdate.Email;
            clientToUpdate.Firstname = clientDto.Firstname ?? clientToUpdate.Firstname;
            clientToUpdate.Lastname = clientDto.Lastname ?? clientToUpdate.Lastname;
            clientToUpdate.Tel = clientDto.Tel ?? clientToUpdate.Tel;
            clientToUpdate.Adress = clientDto.Adress ?? clientToUpdate.Adress;
            clientToUpdate.Cp = clientDto.Cp ?? clientToUpdate.Cp;
            clientToUpdate.City = clientDto.City ?? clientToUpdate.City;
            clientToUpdate.Country = clientDto.Country ?? clientToUpdate.Country;
            if (clientDto.IsActive != null)
            {
                clientToUpdate.IsActive = clientDto.IsActive;
            }

            if (!string.IsNullOrEmpty(clientDto.Password))
            {
                clientToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(clientDto.Password);
            }

            clientToUpdate.Role = role;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "User updated successfully." });
        }



        // ************************ DELETE: 1 by Id ************************
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // ************************ FONCTION: Verify if exist ************************
        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // ************************ FUNCTION : Verify if property is null (for update)  ************************
        public void UpdateProperties<TTarget, TSource>(TTarget target, TSource source)
        {
            var targetProperties = typeof(TTarget).GetProperties();
            var sourceProperties = typeof(TSource).GetProperties();

            foreach (var targetProp in targetProperties)
            {
                var sourceProp = sourceProperties.FirstOrDefault(p => p.Name == targetProp.Name);
                if (sourceProp != null)
                {
                    var value = sourceProp.GetValue(source);
                    if (value != null)
                    {
                        targetProp.SetValue(target, value);
                    }
                }
            }
        }

    }
}
