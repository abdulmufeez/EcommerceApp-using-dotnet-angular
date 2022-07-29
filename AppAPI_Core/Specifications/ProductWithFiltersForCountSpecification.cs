using AppAPI_Core.Entities;

namespace AppAPI_Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productSpecParams)
            : base(x =>
                (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) && 
                (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId)
            )
        {
        }
    }
}