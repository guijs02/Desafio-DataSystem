using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.UseCases.GetAll
{
    public class GetAllTasksUseCase(ITaskRepository repository) : IGetAllTasksUseCase
    {
        public async Task<List<GetAllTasksOutput>> Handle(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            // Implementation to get all tasks with pagination
            var tasks = await repository.GetAllAsync(pageNumber, pageSize, cancellationToken);

            return tasks.Select(task => new GetAllTasksOutput
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                FinishAt = task.FinishAt,
                Status = task.Status
            }).ToList();

        }
    }
}
