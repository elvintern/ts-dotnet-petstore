using backend.Context;
using Microsoft.AspNetCore.Mvc;

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

        // Read

        // Update

        // Delete
    }
}
