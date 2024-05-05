using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eshop.Models;
using eshop.Helpers;
using System.Data;


namespace eshop.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly EshopContext _context;

        public OrderController(EshopContext context)
        {
            _context = context;
        }


        // ************************ GET: all ************************
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            var orders = await _context.Orders.Include(c => c.Client).ThenInclude(c => c.Role).Include(r => r.Status).ToListAsync();

            if (orders.Count == 0)
            {
                return NotFound();
            }

            var OrderDto = orders.Select(c => new OrderDto.find
            {
                Id = c.Id,
                OrderDate = c.OrderDate,
                ValidationDate = c.ValidationDate,
                ShippingDate = c.ShippingDate,
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
                Status = c.Status == null ? null : new StatusDto.model
                {
                    Id = c.Status.Id,
                    Name = c.Status.Name,
                    IsActive = true
                }

            }).ToList();

            return Ok(OrderDto);
        }


        // ************************ GET: By Id ************************
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(c => c.Client)
                    .ThenInclude(c => c.Role)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            var orderDto = new OrderDto.find
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                ValidationDate = order.ValidationDate,
                ShippingDate = order.ShippingDate,
                Client = order.Client == null ? null : new ClientDto.find
                {
                    Id = order.Client.Id,
                    Email = order.Client.Email,
                    Firstname = order.Client.Firstname,
                    Lastname = order.Client.Lastname,
                    Tel = order.Client.Tel,
                    Adress = order.Client.Adress,
                    Cp = order.Client.Cp,
                    City = order.Client.City,
                    Country = order.Client.Country,
                    IsActive = order.Client.IsActive,
                    Role = order.Client.Role == null ? null : new RoleDto.model
                    {
                        Id = order.Client.Role.Id,
                        Name = order.Client.Role.Name,
                        IsActive = order.Client.Role.IsActive
                    }
                },
                Status = order.Status == null ? null : new StatusDto.model
                {
                    Id = order.Status.Id,
                    Name = order.Status.Name,
                    IsActive = true
                }
            };

            return Ok(orderDto);
        }


        // ************************ POST: 1 ************************
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDto.create orderDto)
        {
            var client = await _context.Clients.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == orderDto.Client);
            if (client == null)
            {
                return BadRequest("Invalid Client ID");
            }

            var status = await _context.Statuses.FirstOrDefaultAsync(c => c.Id == orderDto.Status);
            if (status == null)
            {
                return BadRequest("Invalid Status ID");
            }

            Order order = new Order
            {
                OrderDate = (DateTime)orderDto.OrderDate,
                ValidationDate = orderDto.ValidationDate,
                ShippingDate = orderDto.ShippingDate,
                Client = client,
                Status = status
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderDto);
        }


        // ************************ PUT: 1 by Id ************************
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> PutOrder(int id, OrderDto.update orderDto)
        {
            var orderToUpdate = await _context.Orders
                .Include(c => c.Client)
                    .ThenInclude(c => c.Role)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (orderToUpdate == null)
            {
                return NotFound("User not found.");
            }

            var client = await _context.Clients
                .Include(c => c.Role)
                .FirstOrDefaultAsync(c => c.Id == orderDto.Client);

            if (client == null)
            {
                return BadRequest("Invalid Client ID");
            }

            var status = await _context.Statuses.FirstOrDefaultAsync(c => c.Id == orderDto.Status);

            if (status == null)
            {
                return BadRequest("Invalid Product ID");
            }

            orderToUpdate.OrderDate = (DateTime)orderDto.OrderDate;
            orderToUpdate.ValidationDate = orderDto.ValidationDate;
            orderToUpdate.ShippingDate = orderDto.ShippingDate;
            orderToUpdate.Client = client;
            orderToUpdate.Status = status;

            var role = await _context.Roles.FirstOrDefaultAsync(c => c.Id == client.Role.Id);

            if (role != null)
            {
                client.Role = role;
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Order updated successfully." });
        }


        // ************************ DELETE: 1 by Id ************************
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // ************************ FONCTION: Verify if exist ************************
        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
