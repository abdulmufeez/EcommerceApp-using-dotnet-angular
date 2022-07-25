using AppAPI_Core.Entities;
using AppAPI_Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    public class ProductController : BaseRouteController
    {
        private readonly StoreDataContext _context;
        public ProductController(StoreDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync()
        {
            return Ok(await _context.Products.ToListAsync());
        }
    }
}