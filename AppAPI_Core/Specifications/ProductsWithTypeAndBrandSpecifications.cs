using AppAPI_Core.Entities;

namespace AppAPI_Core.Specifications
{
    // giving specification for product related request
    public class ProductsWithTypeAndBrandSpecifications : BaseSpecification<Product>
    {
        public ProductsWithTypeAndBrandSpecifications(ProductSpecParams productSpecParams)
            : base(x =>
                (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) && 
                (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), 
                productSpecParams.PageSize);

            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductsWithTypeAndBrandSpecifications(int id) 
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);        
        }
    }
}