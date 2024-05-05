using eshop.Helpers;
using eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace eshop.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly EshopContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public ProductController(EshopContext context, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }


        // ************************ GET all ************************
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();
        }


        // ************************ GET ONE: By Id ************************
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var productDto = new ProductDto.model
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Height = product.Height,
                Width = product.Width,
                Length = product.Length,
                Weight = product.Weight,
                Capacity = product.Capacity,
                Price = product.Price,
                Maker = product.Maker,
                Color = product.Color,
                Image = product.Image,
                IsActive = product.IsActive
            };

            return Ok(productDto);
        }


        // ************************ GET ALL: By Name ************************
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Product>> GetProductByName(string name)
        {
            var lowerCaseName = name.ToLower();

            var products = await _context.Products
                .Where(p => EF.Functions
                .Like(p.Name.ToLower(), $"%{lowerCaseName}%"))
                .ToListAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound();
            }

            var productDto = products.Select(product => new ProductDto.model
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Height = product.Height,
                Width = product.Width,
                Length = product.Length,
                Weight = product.Weight,
                Capacity = product.Capacity,
                Price = product.Price,
                Maker = product.Maker,
                Color = product.Color,
                Image = product.Image,
                IsActive = product.IsActive
            });

            return Ok(productDto);
        }


        // ************************ POST (Create) ************************
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm] ProductDto.post productDto)
        {
            var baseUrl = _configuration.GetValue<string>("BaseUrl");
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Height = productDto.Height,
                Width = productDto.Width,
                Length = productDto.Length,
                Weight = productDto.Weight,
                Capacity = productDto.Capacity,
                Price = productDto.Price,
                Maker = productDto.Maker,
                Color = productDto.Color,
                IsActive = productDto.IsActive
            };

            if (productDto.Image != null)
            {
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                var filePath = Path.Combine(uploadDir, productDto.Image.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await productDto.Image.CopyToAsync(fileStream);
                }

                product.Image = $"{baseUrl}/Images/{productDto.Image.FileName}";
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }


        // ************************ PUT (update) ************************
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, [FromForm] ProductDto.put productDto)
        {
            var productToUpdate = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (productToUpdate == null)
            {
                return NotFound("Product not found.");
            }

            var imageFile = productDto.Image;
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                var filePath = Path.Combine(uploadDir, productDto.Image.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await productDto.Image.CopyToAsync(fileStream);
                }

                // Construis le chemin relatif pour l'URL de l'image
                var relativePath = Path.Combine("Images", productDto.Image.FileName).Replace("\\", "/");
                var request = HttpContext.Request;
                var host = request.Host.ToUriComponent();
                var scheme = request.Scheme;
                var fullUrl = $"{scheme}://{host}/{relativePath}";

                productToUpdate.Image = fullUrl;
            }

            UpdateProperties(productToUpdate, productDto);

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Product updated successfully." });
        }


        // ************************ DELETE (by Id) ************************
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // ************************ GET ALL: By Order Id  ************************
        [HttpGet("byOrder/{id}")]
        public async Task<ActionResult<IEnumerable<ProductDto.model>>> GetProductsByOrderId(int id)
        {
            var product = await _context.Products
                .Include(o => o.Opinions)
                .Include(p => p.OrderDetails)
                .Where(i => i.OrderDetails.Any(od => od.Order.Id == id))
                .ToListAsync();

            if (product.Count == 0)
            {
                return NotFound();
            }

            var productDto = product.Select(c => new ProductDto.model
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Height = c.Height,
                Width = c.Width,
                Length = c.Length,
                Weight = c.Weight,
                Capacity = c.Capacity,
                Price = c.Price,
                Maker = c.Maker,
                Color = c.Color,
                Image = c.Image,
                IsActive = c.IsActive
            }).ToList();

            return Ok(productDto);
        }




        // ************************ FUNCTION: Verify if exist ************************
        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // ************************ FUNCTION : Verify if property is null (for update)  ************************
        public void UpdateProperties<TTarget, TSource>(TTarget target, TSource source)
        {
            var targetProperties = typeof(TTarget).GetProperties();
            var sourceProperties = typeof(TSource).GetProperties();

            foreach (var targetProp in targetProperties)
            {
                if (targetProp.Name == "Image") continue;  // Ignore the Image property

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
