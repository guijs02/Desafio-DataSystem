using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.UseCases.Update.Input;
using TaskManagement.Application.UseCases.Update.Output;

namespace TaskManagement.Application.Interfaces
{
    public interface IUpdateTaskUseCase
    {
        Task<UpdateTaskOutput> Handle(UpdateTaskInput updateTaskInput, CancellationToken cancellationToken);
    }
}
