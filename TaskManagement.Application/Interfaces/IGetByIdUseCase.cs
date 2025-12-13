using TaskManagement.Application.UseCases.Get;

namespace TaskManagement.Application.Interfaces
{
    public interface IGetByIdUseCase
    {
        Task<GetByIdOutput> Handle(int id, CancellationToken cancellationToken);
    }
}
