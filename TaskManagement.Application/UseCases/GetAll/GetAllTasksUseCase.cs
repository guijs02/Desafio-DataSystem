using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.UseCases.GetAll
{
    public class GetAllTasksUseCase(ITaskRepository repository) : IGetAllTasksUseCase
    {
        public async Task<List<GetAllTasksOutput>> Handle(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var tasks = await repository.GetAllAsync(pageNumber, pageSize, cancellationToken);

            return tasks.Select(task => new GetAllTasksOutput
            (
                task.Id,
                task.Title,
                task.Description!,
                task.CreatedAt,
                task.FinishAt,
                task.Status,
                pageNumber,
                pageSize,
                tasks.Count()
            )).ToList();
        }
    }
}
