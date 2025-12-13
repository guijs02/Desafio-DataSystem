using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Repository;
using TaskManagement.Infraestructure.Context;

namespace TaskManagement.Infraestructure.Persistence
{
    public class TaskRepository(TaskManagementContext context) : ITaskRepository
    {
        public async Task CreateAsync(Domain.Entity.Task task, CancellationToken cancellationToken)
        {
            //add task to database
            await context.Tasks.AddAsync(task, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Domain.Entity.Task task, CancellationToken cancellationToken)
        {
            //delete task from database
            context.Tasks.Remove(task);
            await context.SaveChangesAsync(cancellationToken);

        }

        public async Task<IEnumerable<Domain.Entity.Task>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            //get all tasks from database with pagination
            var tasks = await context.Tasks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return tasks;
        }

        public async Task<Domain.Entity.Task?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            //get task from database
            return await context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Domain.Entity.Task task, CancellationToken cancellationToken)
        {
            //update task in database
            context.Tasks.Update(task);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
