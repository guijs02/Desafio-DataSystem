namespace TaskManagement.Application.Interfaces
{
    public interface IDeleteTaskUseCase
    {
        Task Handle(int id, CancellationToken cancellationToken);
    }
}
