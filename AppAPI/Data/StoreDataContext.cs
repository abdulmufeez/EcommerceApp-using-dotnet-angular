using AppAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Data
{
    public class StoreDataContext : DbContext
    {
        public StoreDataContext(DbContextOptions<StoreDataContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}