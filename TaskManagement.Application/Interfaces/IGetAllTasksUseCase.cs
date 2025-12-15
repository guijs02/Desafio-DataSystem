using TaskManagement.Application.UseCases.GetAll;

namespace TaskManagement.Application.Interfaces
{
    public interface IGetAllTasksUseCase
    {
        Task<List<GetAllTasksOutput>> Handle(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
