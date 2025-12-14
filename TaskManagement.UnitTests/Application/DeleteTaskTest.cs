using Moq;
using TaskManagement.Application.UseCases.Delete;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Repository;
using Task = System.Threading.Tasks.Task;
using TaskEntity = TaskManagement.Domain.Entity.Task;

namespace TaskManagement.UnitTests.Application
{
    public class DeleteTaskTest
    {
        //should create tests for DeleteTask use case
        [Fact(DisplayName = nameof(ShouldDeleteTaskFromRepository))]
        public async Task ShouldDeleteTaskFromRepository()
        {
            // Arrange
            var repositoryMock = new Mock<ITaskRepository>();
            var task = new TaskEntity(
                "Test Task",
                "This is a test task",
                Status.Pending,
                DateTime.Now
            );

            repositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(task);

            var useCase = new DeleteTaskUseCase(repositoryMock.Object);
            // Act
            await useCase.Handle(1, CancellationToken.None);
            // Assert
            repositoryMock.Verify(x => x.DeleteAsync(task, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
