
using Ecommerce.Api.Extensions;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.ProductModule;
using Ecommerce.Persistence.Data;
using Ecommerce.Persistence.Data.SeedData;
using Ecommerce.Persistence.Repositories;
using Ecommerce.Service.Abstraction;
using Ecommerce.Services;
using Ecommerce.Services.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Ecommerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region DI
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //Injecting DbContext
            builder.Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );

            builder.Services.AddScoped<IDataInitializer, DataInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped <IProductService, ProductsService>();
            //Inject Mapping profile
            builder.Services.AddAutoMapper(x => x.AddProfile<ProductProfile>());
            #endregion

            // Configure the HTTP request pipeline.
            var app = builder.Build();

            #region Seeding Data
            await app.MigrateDatabase();
            await app.SeedData(); 
            #endregion

            #region Configuring PipeLine(MiddleWares)
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}
