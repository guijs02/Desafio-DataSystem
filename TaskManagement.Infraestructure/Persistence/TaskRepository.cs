using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Repository;
using TaskManagement.Infraestructure.Context;
using TaskEntity = TaskManagement.Domain.Entity.Task;

namespace TaskManagement.Infraestructure.Persistence
{
    public class TaskRepository(TaskManagementContext context) : ITaskRepository
    {
        public async Task CreateAsync(TaskEntity task, CancellationToken cancellationToken)
        {
            await context.Tasks.AddAsync(task, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(TaskEntity task, CancellationToken cancellationToken)
        {
            context.Tasks.Remove(task);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await context.Tasks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken)
        {
            context.Tasks.Update(task);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
