using TaskManagement.Application.Interfaces;
using TaskManagement.Application.UseCases.Update.Input;
using TaskManagement.Application.UseCases.Update.Output;
using TaskManagement.Domain.Exceptions;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.UseCases.Update
{
    public class UpdateTaskUseCase(ITaskRepository repository) : IUpdateTaskUseCase
    {
        public async Task<UpdateTaskOutput> Handle(UpdateTaskInput updateTaskInput, CancellationToken cancellationToken)
        {
            var task = await repository.GetByIdAsync(updateTaskInput.Id, cancellationToken);

            if (task == null)
            {
                throw new TaskNotFoundException($"Task with ID {updateTaskInput.Id} not found.");
            }

            task.UpdateTitle(updateTaskInput.Title);
            task.UpdateDescription(updateTaskInput.Description);
            task.UpdateFinishAt(updateTaskInput.FinishAt);
            task.UpdateStatus(updateTaskInput.Status);

            await repository.UpdateAsync(task, cancellationToken);

            return new UpdateTaskOutput
            (
                task.Id,
                task.Title,
                task.Description,
                task.FinishAt,
                task.Status
            );
        }
    }
}
