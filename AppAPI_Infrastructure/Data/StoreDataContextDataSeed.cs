using System.Text.Json;
using AppAPI_Core.Entities;
using Microsoft.Extensions.Logging;

namespace AppAPI_Infrastructure.Data
{
    public class StoreDataContextDataSeed
    {
        public static async Task SeedAsync(StoreDataContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandData = 
                        await File.ReadAllTextAsync("../AppAPI_Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var brandTypeData = 
                        await File.ReadAllTextAsync("../AppAPI_Infrastructure/Data/SeedData/types.json");

                    var brandTypes = JsonSerializer.Deserialize<List<ProductType>>(brandTypeData);

                    foreach (var item in brandTypes)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productData = 
                        await File.ReadAllTextAsync("../AppAPI_Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreDataContext>();
                logger.LogError(ex, ex.Message);
            }
        }
    }
}