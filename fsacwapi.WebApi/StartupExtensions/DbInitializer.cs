using fsacwapi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace fsacwapi.WebApi.StartupExtensions;

public static class DbInitializer
{
    public static void InitializeDatabase(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DBContext>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<DBContext>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}