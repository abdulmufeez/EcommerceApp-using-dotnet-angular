using AppAPI_Core.Entities;

namespace AppAPI_Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
    }
}