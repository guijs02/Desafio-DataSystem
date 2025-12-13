using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.UseCases.Create.Input;
using TaskManagement.Application.UseCases.Create.Output;
using TaskManagement.Domain.Repository;
using Task = TaskManagement.Domain.Entity.Task;

namespace TaskManagement.Application.UseCases.Create
{
    public class CreateTaskUseCase(ITaskRepository repository) : ICreateTaskUseCase
    {
        public async Task<CreateTaskOutput> Handle(CreateTaskInput input, CancellationToken cancellationToken)
        {
            var task = new Task(input.Title, input.Description, input.Status, input.FinishAt);

            await repository.CreateAsync(task, cancellationToken);

            return new CreateTaskOutput(task.Id, input.Title, input.Description, task.CreatedAt, input.FinishAt, input.Status);
        }
    }
}
