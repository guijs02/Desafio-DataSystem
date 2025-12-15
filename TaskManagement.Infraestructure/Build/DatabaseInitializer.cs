using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infraestructure.Context;

namespace TaskManagement.Infraestructure.Build
{
    public static class DatabaseInitializer
    {
        public static async Task EnsureDatabaseCreated(this IServiceProvider service)
        {
            using var scope = service.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<TaskManagementContext>();

            if (!await db.Database.CanConnectAsync())
            {
                db.Database.Migrate();
            }
        }
    }
}
