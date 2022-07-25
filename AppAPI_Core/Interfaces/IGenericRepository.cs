using AppAPI_Core.Entities;

namespace AppAPI_Core.Interfaces
{   
    // applying constraint as where T : baseEntity 
    // this generic only implememted for entities only
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
    }
}