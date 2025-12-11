using System.Collections.Generic;
using TaskManagement.Domain.Entity;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Domain.Repository
{
    public interface ITaskRepository
    {
        Task CreateAsync(Entity.Task task, CancellationToken cancellationToken);

        Task<Entity.Task?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Entity.Task>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task UpdateAsync(Entity.Task task, CancellationToken cancellationToken);

        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
