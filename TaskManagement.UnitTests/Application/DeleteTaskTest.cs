using FluentAssertions;
using Moq;
using TaskManagement.Application.UseCases.Delete;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Exceptions;
using TaskManagement.Domain.Repository;
using Task = System.Threading.Tasks.Task;
using TaskEntity = TaskManagement.Domain.Entity.Task;

namespace TaskManagement.UnitTests.Application
{
    public class DeleteTaskTest
    {
        [Fact(DisplayName = nameof(ShouldDeleteTaskFromRepository))]
        public async Task ShouldDeleteTaskFromRepository()
        {
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

            await useCase.Handle(1, CancellationToken.None);

            repositoryMock.Verify(x => x.DeleteAsync(task, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = nameof(ShouldThrowExceptionWhenTaskNotFound))]
        public async Task ShouldThrowExceptionWhenTaskNotFound()
        {

            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((TaskEntity?)null);

            var useCase = new DeleteTaskUseCase(repositoryMock.Object);

            var act = () => useCase.Handle(1, CancellationToken.None);

            await act.Should().ThrowAsync<TaskNotFoundException>();

            repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<TaskEntity>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
