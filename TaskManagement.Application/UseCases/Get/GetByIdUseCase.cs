using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Exceptions;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.UseCases.Get
{
    public class GetByIdUseCase(ITaskRepository repository) : IGetByIdUseCase
    {
        public async Task<GetByIdOutput> Handle(int id, CancellationToken cancellationToken)
        {
            // Implementation goes here
            var task = await repository.GetByIdAsync(id, cancellationToken);

            if (task == null)
            {
                throw new TaskNotFoundException($"Task with ID {id} not found.");
            }

            return new GetByIdOutput
            (
                task.Id,
                task.Title,
                task.Description,
                task.CreatedAt,
                task.FinishAt,
                task.Status
            );
        }
    }
}
