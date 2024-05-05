using eshop.Helpers;
using eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace eshop.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {

        private readonly EshopContext _context;

        public AuthController(EshopContext context)
        {
            _context = context;
        }


        // ************************ FONCTION: Login ************************
        [HttpPost("login")]
        public async Task<ActionResult> Login(ClientDto.login clientDto)
        {
            var client = await _context.Clients
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == clientDto.Email);

            if (client == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(clientDto.Password, client.Password);

            if (!isValidPassword)
            {
                return Unauthorized("Invalid email or password.");
            }

            var userWithRoleDto = new ClientDto.find
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

            return Ok(userWithRoleDto);
        }


        // ************************ FONCTION: Inscription ************************
        [HttpPost("register")]
        public async Task<ActionResult> Register(ClientDto.register clientDto)
        {

            if (clientDto.Password != clientDto.ConfirmPassword)
            {
                return BadRequest("Password and Confirm Password are differents.");
            }

            var existingClient = await _context.Clients.FirstOrDefaultAsync(u => u.Email == clientDto.Email);
            if (existingClient != null)
            {
                return BadRequest("Email already in use.");
            }

            var role = await _context.Roles.Include(r => r.Clients).FirstOrDefaultAsync(c => c.Id == 1);
            if (role == null)
            {
                return BadRequest("Invalid Role ID");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(clientDto.Password);

            Client client = new Client
            {
                Email = clientDto.Email,
                Password = hashedPassword,
                IsActive = true,
                Role = role
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Registration successful" });
        }


        // ************************ FONCTION: Verify if Client exist ************************
        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
