using backend.Context;
using backend.Dtos;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductsController (ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateProductDto dto)
        {
            var newProduct = new ProductEntity()
            {
                Brand = dto.Brand,
                Title = dto.Title,
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return Ok("Product Saved Successfully");
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<List<ProductEntity>>> GetProducts()
        {
            var products = await _context.Products.OrderByDescending(p => p.UpdatedAt).ToListAsync();

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductEntity>> GetProductById([FromRoute] long id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product is not found");
            }

            return Ok(product);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] long id, [FromBody] CreateUpdateProductDto dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product is not found");
            }

            product.Title = dto.Title;
            product.Brand = dto.Brand;
            product.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Product Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] long id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product is not found");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Product Updated Successfully");
        }
    }
}
