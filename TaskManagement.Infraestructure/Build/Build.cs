using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Domain.Repository;
using TaskManagement.Infraestructure.Context;
using TaskManagement.Infraestructure.Persistence;

namespace TaskManagement.Infraestructure.Build
{
    public static class Build
    {
        public static IServiceCollection AddContext(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            service.AddDbContext<TaskManagementContext>(opts => 
            opts.UseSqlServer(connectionString, b => b.MigrationsAssembly("TaskManagement.Infraestructure")));
            return service;
        }

        public static IServiceCollection AddInfraDependencies(this IServiceCollection service)
        {
            service.AddScoped<ITaskRepository, TaskRepository>();

            return service;
        }

    }
}
