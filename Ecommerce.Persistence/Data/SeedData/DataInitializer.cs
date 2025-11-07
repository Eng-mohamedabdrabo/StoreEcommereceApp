using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Persistence.Data.SeedData
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;

        public DataInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            var hasProduct = await _dbContext.Products.AnyAsync();
            var hasBrand = await _dbContext.ProductBrands.AnyAsync();
            var hasTypes = await _dbContext.ProductTypes.AnyAsync();
            if (hasProduct && hasBrand && hasTypes)
                return;

            try
            {
                if (!hasBrand)
                {
                   await SeedDataFromJson<ProductBrand, int>("brands.json", _dbContext.ProductBrands);
                }
                if (!hasTypes)
                {
                   await SeedDataFromJson<ProductType, int>("types.json", _dbContext.ProductTypes);
                }
                _dbContext.SaveChanges();
                if (!hasProduct)
                {
                    await SeedDataFromJson<Products, int>("products.json", _dbContext.Products);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Initializing {ex}");
                throw;
            }
        }

        private async Task SeedDataFromJson<T, TKey>(string fileName, DbSet<T> dbSet) where T : BaseEntity<TKey>
        {
            var filePath = @"..\Ecommerce.Persistence\Data\SeedData\SeedingFiles\" + fileName;
            if (!File.Exists(filePath)) 
                throw new FileNotFoundException("File Not Found",filePath);

            try
            {
                var dataStream = File.OpenRead(filePath);
                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (data is not null)
                    dbSet.AddRange(data);
            }
            catch (Exception exc)
            {

                Console.WriteLine($"Error While Seeding Data of {nameof(dbSet)} with Exception {exc}");
            }

        }
    }
}
