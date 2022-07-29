using AppAPI_Core.Entities;

namespace AppAPI_Core.Specifications
{
    // giving specification for product related request
    public class ProductsWithTypeAndBrandSpecifications : BaseSpecification<Product>
    {
        public ProductsWithTypeAndBrandSpecifications(string sort, int? brandId, int? typeId)
            : base(x =>
                (!brandId.HasValue || x.ProductBrandId == brandId) && 
                (!typeId.HasValue || x.ProductTypeId == typeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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