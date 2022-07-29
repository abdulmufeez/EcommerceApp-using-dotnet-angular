using AppAPI.Dtos;
using AppAPI.Errors;
using AppAPI.Helpers;
using AppAPI_Core.Entities;
using AppAPI_Core.Interfaces;
using AppAPI_Core.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    public class ProductsController : BaseRouteController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<Product>>> GetProductsAsync(
            [FromQuery] ProductSpecParams productSpecParams)
        {
            var spec = new ProductsWithTypeAndBrandSpecifications(productSpecParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productSpecParams);

            var totalItems = await _productRepo.CountAsync(countSpec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(await _productRepo.ListAsync(spec));

            return Ok(new Pagination<ProductToReturnDto>(productSpecParams.PageIndex, productSpecParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        // proper documenting for swagger
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithTypeAndBrandSpecifications(id);
            var product = await _productRepo.GetEntityWithSpecs(spec);
            if (product is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product, ProductToReturnDto>
                (product));
        }
    }
}