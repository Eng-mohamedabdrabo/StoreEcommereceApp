using Ecommerce.Domain.Contracts;
using Ecommerce.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Extensions
{
    public static class WebApplicationRegister
    {
        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();

            try
            {
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<StoreDbContext>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }

            return app;
        }

        public static WebApplication SeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dataInitializer = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
            dataInitializer.Initialize();
            return app;
        }
    }
}
