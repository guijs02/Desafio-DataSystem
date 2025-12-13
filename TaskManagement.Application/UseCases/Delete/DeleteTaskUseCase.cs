using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Exceptions;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.UseCases.Delete
{
    public class DeleteTaskUseCase(ITaskRepository repository) : IDeleteTaskUseCase
    {
        public async Task Handle(int id, CancellationToken cancellationToken)
        {
            var task = await repository.GetByIdAsync(id, cancellationToken);

            if (task == null)
            {
                throw new TaskNotFoundException($"Task with id {id} not found.");
            }

            await repository.DeleteAsync(task, cancellationToken);
        }
    }
}
