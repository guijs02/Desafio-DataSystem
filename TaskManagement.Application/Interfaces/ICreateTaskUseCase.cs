using TaskManagement.Application.UseCases.Create.Input;
using TaskManagement.Application.UseCases.Create.Output;

namespace TaskManagement.Application.Interfaces
{
    public interface ICreateTaskUseCase
    {
        Task<CreateTaskOutput> Handle(CreateTaskInput input, CancellationToken cancellationToken);
    }
}
