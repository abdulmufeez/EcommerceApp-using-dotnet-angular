using System.Reflection;
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
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // reading the config file with in the project assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}