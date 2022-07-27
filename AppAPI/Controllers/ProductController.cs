using AppAPI_Core.Entities;
using AppAPI_Core.Interfaces;
using AppAPI_Core.Specifications;
using AppAPI_Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    public class ProductController : BaseRouteController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductController(IGenericRepository<Product> productRepo) 
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProductsAsync()
        {
            var Spec = new ProductsWithTypeAndBrandSpecifications();
            return Ok(await _productRepo.ListAsync(Spec));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithTypeAndBrandSpecifications(id);
            return Ok(await _productRepo.GetEntityWithSpecs(spec));
        }
    }
}