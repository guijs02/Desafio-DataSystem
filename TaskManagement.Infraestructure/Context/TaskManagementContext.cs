using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Infraestructure.Context
{
    public class TaskManagementContext(DbContextOptions<TaskManagementContext> options) : DbContext(options)
    {
        public DbSet<Domain.Entity.Task> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Domain.Entity.Task>(entity =>
            {
                entity.ToTable("Tasks");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Description)
                    .HasMaxLength(500);
                entity.Property(e => e.CreatedAt)
                    .IsRequired();
                entity.Property(e => e.FinishAt);
                entity.Property(e => e.Status)
                    .IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
