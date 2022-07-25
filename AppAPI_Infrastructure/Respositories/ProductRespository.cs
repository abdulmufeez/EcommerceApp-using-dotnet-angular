using AppAPI_Core.Entities;
using AppAPI_Core.Interfaces;
using AppAPI_Infrastructure.Data;

namespace AppAPI_Core.Respositories
{
    public class ProductRespository : IProductRepository
    {
        private readonly StoreDataContext _context;
        public ProductRespository(StoreDataContext context)
        {
            _context = context;
        }

        public Task<Product> GetProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}