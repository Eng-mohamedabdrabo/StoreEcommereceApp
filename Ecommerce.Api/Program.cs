using Ecommerce.Api.Extensions;
using Ecommerce.Domain.Contracts;
using Ecommerce.Persistence.Data;
using Ecommerce.Persistence.Data.SeedData;
using Ecommerce.Persistence.Repositories;
using Ecommerce.Service.Abstraction;
using Ecommerce.Services;
using Ecommerce.Services.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using StackExchange.Redis;

namespace Ecommerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ecommerce API",
                    Version = "v1"
                });
            });

            builder.Services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IDataInitializer, DataInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductsService>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<ICacheRepository, CacheRepository>();
            builder.Services.AddScoped<ICacheService, CacheService>();

            builder.Services.AddAutoMapper(typeof(MappingServiceReference).Assembly);

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            }
            );

            var app = builder.Build();

            await app.MigrateDatabase();
            await app.SeedData();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
