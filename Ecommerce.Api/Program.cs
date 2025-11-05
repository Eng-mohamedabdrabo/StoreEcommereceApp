
using Ecommerce.Api.Extensions;
using Ecommerce.Domain.Contracts;
using Ecommerce.Persistence.Data;
using Ecommerce.Persistence.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Ecommerce.Api
{
    public class Program
    {
        public static void Main(string[] args)
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

            #endregion
            // Configure the HTTP request pipeline.
            var app = builder.Build();

            #region Seeding Data
            app.MigrateDatabase();
            app.SeedData(); 
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
