using AppAPI_Core.Entities;

namespace AppAPI_Core.Specifications
{
    // giving specification for product related request
    public class ProductsWithTypeAndBrandSpecifications : BaseSpecification<Product>
    {
        public ProductsWithTypeAndBrandSpecifications()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        public ProductsWithTypeAndBrandSpecifications(int id) 
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);        
        }
    }
}