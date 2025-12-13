using Microsoft.EntityFrameworkCore;
using TaskManagement.Infraestructure.Mapping;
using TaskEntity = TaskManagement.Domain.Entity.Task;

namespace TaskManagement.Infraestructure.Context
{
    public class TaskManagementContext(DbContextOptions<TaskManagementContext> options) : DbContext(options)
    {
        public DbSet<TaskEntity> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
