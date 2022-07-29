using AppAPI_Core.Entities;
using AppAPI_Core.Specifications;

namespace AppAPI_Core.Interfaces
{   
    // applying constraint as where T : baseEntity 
    // this generic only implememted for entities only
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpecs(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec);
    }
}