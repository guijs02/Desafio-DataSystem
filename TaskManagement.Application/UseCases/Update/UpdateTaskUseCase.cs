using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.UseCases.Update.Input;
using TaskManagement.Application.UseCases.Update.Output;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.UseCases.Update
{
    public class UpdateTaskUseCase(ITaskRepository repository) : IUpdateTaskUseCase
    {
        public async Task<UpdateTaskOutput> Handle(UpdateTaskInput updateTaskInput, CancellationToken cancellationToken)
        {
            //should get the task from the repository
            var task = await repository.GetByIdAsync(updateTaskInput.Id, cancellationToken);

            if (task == null)
            {
                throw new InvalidOperationException();
            }

            //should update the task properties
            task.UpdateTitle(updateTaskInput.Title);
            task.UpdateDescription(updateTaskInput.Description);
            task.UpdateFinishAt(updateTaskInput.FinishAt);

            //should save the updated task to the repository
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
