using AppAPI_Core.Entities;
using AppAPI_Core.Interfaces;
using AppAPI_Core.Specifications;
using AppAPI_Infrastructure.Data;
using AppAPI_Infrastructure.SpecEvaluator;
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
        
        public async Task<T> GetEntityWithSpecs(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        // helper method to evaluate spec with db
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}