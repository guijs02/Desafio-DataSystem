using TaskManagement.Application.UseCases.GetAll;

namespace TaskManagement.Application.Interfaces
{
    public interface IGetAllTasksUseCase
    {
        // Method to get all tasks with pagination
        Task<List<GetAllTasksOutput>> Handle(int pageNumber, int pageSize, CancellationToken cancellationToken);

    }
}
