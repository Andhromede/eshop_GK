using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eshop.Models;
using eshop.Helpers;
using System.Data;
using static NuGet.Packaging.PackagingConstants;


namespace eshop.Controllers
{
    [ApiController]
    [Route("orderDetails")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly EshopContext _context;

        public OrderDetailsController(EshopContext context)
        {
            _context = context;
        }


        // ************************ GET: all ************************
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetails>>> GetOrderDetails()
        {
            var ordersDetails = await _context.OrderDetails
                .Include(c => c.Order)
                    .ThenInclude(c => c.Client)
                .Include(c => c.Order)
                    .ThenInclude(c => c.Status)
                .Include(r => r.Product)
                .ToListAsync();

            if (ordersDetails.Count == 0)
            {
                return NotFound();
            }

            var ordersDetailsDto = ordersDetails.Select(c => new OrderDetailsDto.find
            {
                Id = c.Id,
                Quantity = c.Quantity,
                Price = c.Price,
                Order = c.Order == null ? null : new OrderDto.find
                {
                    Id = c.Id,
                    OrderDate = c.Order.OrderDate,
                    ValidationDate = c.Order.ValidationDate,
                    ShippingDate = c.Order.ShippingDate,
                    Client = c.Order.Client == null ? null : new ClientDto.find
                    {
                        Id = c.Order.Client.Id,
                        Email = c.Order.Client.Email,
                        Firstname = c.Order.Client.Firstname,
                        Lastname = c.Order.Client.Lastname,
                        Tel = c.Order.Client.Tel,
                        Adress = c.Order.Client.Adress,
                        Cp = c.Order.Client.Cp,
                        City = c.Order.Client.City,
                        Country = c.Order.Client.Country,
                        IsActive = c.Order.Client.IsActive,
                    },
                    Status = c.Order.Status == null ? null : new StatusDto.model{
                        Id = c.Order.Status.Id,
                        Name = c.Order.Status.Name,
                        IsActive = c.Order.Status.IsActive
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
                    Image = c.Product.Image,
                    Color = c.Product.Color,
                    IsActive = true
                }
            }).ToList();

            return Ok(ordersDetailsDto);
        }


        // ************************ GET: By Id  ************************
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetails>> GetOrderDetailsById(int id)
        {
            var orders = await _context.OrderDetails
                .Include(c => c.Product)
                .Include(c => c.Order)
                    .ThenInclude(c => c.Status)
                .Where(c => c.Id == id)
                .ToListAsync();

            if (orders.Count == 0)
            {
                return NotFound();
            }

            var order = orders.Select(c => new OrderDetailsDto.find
            {
                Id = c.Id,
                Quantity = c.Quantity,
                Price = c.Price,
                Order = c.Order == null ? null : new OrderDto.find
                {
                    Id = c.Id,
                    OrderDate = c.Order.OrderDate,
                    ValidationDate = c.Order.ValidationDate,
                    ShippingDate = c.Order.ShippingDate,
                    Client = c.Order.Client == null ? null : new ClientDto.find
                    {
                        Id = c.Order.Client.Id,
                        Email = c.Order.Client.Email,
                        Firstname = c.Order.Client.Firstname,
                        Lastname = c.Order.Client.Lastname,
                        Tel = c.Order.Client.Tel,
                        Adress = c.Order.Client.Adress,
                        Cp = c.Order.Client.Cp,
                        City = c.Order.Client.City,
                        Country = c.Order.Client.Country,
                        IsActive = c.Order.Client.IsActive,
                    },
                    Status = c.Order.Status == null ? null : new StatusDto.model
                    {
                        Id = c.Order.Status.Id,
                        Name = c.Order.Status.Name,
                        IsActive = c.Order.Status.IsActive
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
                    Image = c.Product.Image,
                    Color = c.Product.Color,
                    IsActive = true
                }
            }).ToList();

            return Ok(order);
        }


        // ************************ GET ALL: By Order Id  ************************
        [HttpGet("byOrder/{id}")]
        public async Task<ActionResult<OrderDetails>> GetOrderDetailsByOrderId(int id)
        {
            var orders = await _context.OrderDetails
                .Include(c => c.Product)
                .Include(c => c.Order)
                    .ThenInclude(c => c.Status)
                .Where(c => c.Order.Id == id)
                .ToListAsync();

            if (orders.Count == 0)
            {
                return NotFound();
            }

            var order = orders.Select(c => new OrderDetailsDto.find
            {
                Id = c.Id,
                Quantity = c.Quantity,
                Price = c.Price,
                Order = c.Order == null ? null : new OrderDto.find
                {
                    Id = c.Order.Id,
                    OrderDate = c.Order.OrderDate,
                    ValidationDate = c.Order.ValidationDate,
                    ShippingDate = c.Order.ShippingDate,
                    Client = c.Order.Client == null ? null : new ClientDto.find
                    {
                        Id = c.Order.Client.Id,
                        Email = c.Order.Client.Email,
                        Firstname = c.Order.Client.Firstname,
                        Lastname = c.Order.Client.Lastname,
                        Tel = c.Order.Client.Tel,
                        Adress = c.Order.Client.Adress,
                        Cp = c.Order.Client.Cp,
                        City = c.Order.Client.City,
                        Country = c.Order.Client.Country,
                        IsActive = c.Order.Client.IsActive,
                    },
                    Status = c.Order.Status == null ? null : new StatusDto.model
                    {
                        Id = c.Order.Status.Id,
                        Name = c.Order.Status.Name,
                        IsActive = c.Order.Status.IsActive
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
                    Image = c.Product.Image,
                    Color = c.Product.Color,
                    IsActive = true
                }
            }).ToList();

            return Ok(order);
        }


        // ************************ POST: 1 ************************
        [HttpPost]
        public async Task<ActionResult<OrderDetails>> PostOrder([FromBody] OrderDetailsDto.create orderDetailsDto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == orderDetailsDto.Product);
            var order = await _context.Orders.FirstOrDefaultAsync(c => c.Id == orderDetailsDto.Order);
            if (product == null || order == null)
            {
                return BadRequest("Invalid Product or Order ID");
            }

            OrderDetails orderDetails = new OrderDetails
            {
                Quantity = orderDetailsDto.Quantity,
                Price = orderDetailsDto.Price,
                Product = product,
                Order = order
            };

            _context.OrderDetails.Add(orderDetails);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrderDetails), new { id = orderDetails.Id }, orderDetailsDto);
        }


        // ************************ PUT: 1 by Id ************************
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDetails>> PutOrderDetails(int id, OrderDetailsDto.update orderDetailsDto)
        {
            var orderDetailToUpdate = await _context.OrderDetails
                .Include(c => c.Order)
                    .ThenInclude(c => c.Client)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (orderDetailToUpdate == null)
            {
                return NotFound("OrderDetails not found.");
            }

            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == orderDetailsDto.Product);
            var order = await _context.Orders.Include(c => c.Client).FirstOrDefaultAsync(c => c.Id == orderDetailsDto.Order);

            if (product == null || order == null)
            {
                return BadRequest("Product or Order ID is not valid");
            }

            orderDetailToUpdate.Quantity = orderDetailsDto.Quantity;
            orderDetailToUpdate.Price = orderDetailsDto.Price;
            orderDetailToUpdate.Product = product;
            orderDetailToUpdate.Order = order;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "OrderDetails updated successfully." });
        }


        // ************************ DELETE: 1 by Id ************************
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            var orderDetails = await _context.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetails);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // ************************ FONCTION: Verify if exist ************************
        private bool OrderDetailsExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
