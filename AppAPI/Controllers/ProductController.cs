using AppAPI_Core.Entities;
using AppAPI_Core.Interfaces;
using AppAPI_Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    public class ProductController : BaseRouteController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        public ProductController(IGenericRepository<Product> productRepo, 
            IGenericRepository<ProductBrand> productBrandRepo, 
            IGenericRepository<ProductType> productTypeRepo) 
        {
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProductsAsync()
        {
            return Ok(await _productRepo.ListAllAsync());
        }
    }
}