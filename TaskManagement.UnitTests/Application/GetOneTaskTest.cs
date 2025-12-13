using FluentAssertions;
using Moq;
using TaskManagement.Application.UseCases.Get;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Exceptions;
using TaskManagement.Domain.Repository;
using Task = System.Threading.Tasks.Task;
using TaskEntity = TaskManagement.Domain.Entity.Task;

namespace TaskManagement.UnitTests.Application
{
    public class GetOneTaskTest
    {
        //should get one task from repository
        [Fact(DisplayName = nameof(ShouldGetOneTaskFromRepository))]
        public async Task ShouldGetOneTaskFromRepository()
        {
            var repositoryMock = new Mock<ITaskRepository>();

            var taskId = 1;

            var useCase = new GetByIdUseCase(repositoryMock.Object);

            //should setup repository to return a task when GetByIdAsync is called
            repositoryMock.Setup(x => x.GetByIdAsync(taskId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new TaskEntity
                (
                    "Test Task",
                    "This is a test task",
                    Status.Pending
                ));

            var output = await useCase.Handle(taskId, CancellationToken.None);

            output.Should().NotBeNull();
            output.Title.Should().Be("Test Task");
            output.Description.Should().Be("This is a test task");
            output.Status.Should().Be(Status.Pending);

            repositoryMock.Verify(x => x.GetByIdAsync(taskId, It.IsAny<CancellationToken>()), Times.Once);
        }

        //should throw KeyNotFoundException when task not found
        [Fact(DisplayName = nameof(ShouldThrowTaskNotFoundExceptionWhenTaskNotFound))]
        public async Task ShouldThrowTaskNotFoundExceptionWhenTaskNotFound()
        {
            var repositoryMock = new Mock<ITaskRepository>();
            var taskId = 1;
            var useCase = new GetByIdUseCase(repositoryMock.Object);

            //should setup repository to return null when GetByIdAsync is called
            repositoryMock.Setup(x => x.GetByIdAsync(taskId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((TaskEntity?)null);

            var act = async () => await useCase.Handle(taskId, CancellationToken.None);

            await act.Should().ThrowAsync<TaskNotFoundException>()
                .WithMessage($"Task with ID {taskId} not found.");

            repositoryMock.Verify(x => x.GetByIdAsync(taskId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
