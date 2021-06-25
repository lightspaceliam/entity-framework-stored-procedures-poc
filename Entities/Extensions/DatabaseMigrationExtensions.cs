using Entities.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace Entities.Extensions
{
    public static class DatabaseMigrationExtensions
    {
        public static void MigrateDatabase(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureServices(services =>
            {
                var context = services
                    .BuildServiceProvider()
                    .GetRequiredService<EfStoredProcedureDbContext>();

                var logger = services
                    .BuildServiceProvider()
                    .GetRequiredService<ILogger<IWebHost>>();

                try
                {
                    logger.LogInformation("Running database migrations.");
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    logger.LogError("Something went wrong with auto migrations", ex);
                }
            });
        }
    }
}
