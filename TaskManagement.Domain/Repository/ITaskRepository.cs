using Task = System.Threading.Tasks.Task;
using TaskEntity = TaskManagement.Domain.Entity.Task;

namespace TaskManagement.Domain.Repository
{
    public interface ITaskRepository
    {
        Task CreateAsync(TaskEntity task, CancellationToken cancellationToken);

        Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<TaskEntity>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken);

        Task DeleteAsync(TaskEntity task, CancellationToken cancellationToken);
    }
}
