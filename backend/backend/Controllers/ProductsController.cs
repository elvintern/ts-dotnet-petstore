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
            var Products = await _context.Products.ToListAsync();

            return Ok(Products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductEntity>> GetProductById([FromRoute] long id)
        {
            var Product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (Product is null)
            {
                return NotFound("Product is not found");
            }

            return Ok(Product);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] long id, [FromBody] CreateUpdateProductDto dto)
        {
            var Product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (Product is null)
            {
                return NotFound("Product is not found");
            }

            Product.Title = dto.Title;
            Product.Brand = dto.Brand;

            await _context.SaveChangesAsync();

            return Ok("Product Updated Successfully");
        }



        // Delete
    }
}
