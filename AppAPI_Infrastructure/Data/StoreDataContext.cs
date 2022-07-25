using AppAPI_Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppAPI_Infrastructure.Data
{
    public class StoreDataContext : DbContext
    {
        public StoreDataContext(DbContextOptions<StoreDataContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}