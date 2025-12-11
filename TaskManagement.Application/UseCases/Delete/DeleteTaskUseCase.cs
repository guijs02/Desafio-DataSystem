using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.UseCases.Delete
{
    public class DeleteTaskUseCase(ITaskRepository repository) : IDeleteTaskUseCase
    {
        public async Task Handle(int id, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(id, cancellationToken);
        }
    }
}
