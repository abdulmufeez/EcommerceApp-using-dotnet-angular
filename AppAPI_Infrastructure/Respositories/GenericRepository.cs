using AppAPI_Core.Entities;
using AppAPI_Core.Interfaces;
using AppAPI_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppAPI_Infrastructure.Respositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDataContext _context;
        public GenericRepository(StoreDataContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}