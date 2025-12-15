using FluentAssertions;
using Moq;
using TaskManagement.Application.UseCases.GetAll;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Repository;
using Task = System.Threading.Tasks.Task;
using TaskEntity = TaskManagement.Domain.Entity.Task;

namespace TaskManagement.UnitTests.Application
{
    public class GetAllTaskTest
    {
        [Fact(DisplayName = nameof(ShouldGetAllTasksFromRepository))]
        public async Task ShouldGetAllTasksFromRepository()
        {
            // Arrange
            var repositoryMock = new Mock<ITaskRepository>();
            var useCase = new GetAllTasksUseCase(repositoryMock.Object);
            var tasks = new List<TaskEntity>
            {
                new(
                    "Task 1",
                    "Description 1",
                    Status.Pending,
                    DateTime.Now
                ),
                new(
                    "Task 2",
                    "Description 2",
                    Status.Completed,
                    DateTime.Now
                )
            };

            repositoryMock
                .Setup(x =>
                    x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(tasks);

            var output = await useCase.Handle(1, 10, CancellationToken.None);

            output.Should().NotBeNull();
            output.Count.Should().Be(2);
            output[0].Title.Should().Be("Task 1");
            output[1].Title.Should().Be("Task 2");
            output[0].Description.Should().Be("Description 1");
            output[1].Description.Should().Be("Description 2");
            output[0].Status.Should().Be(Status.Pending);
            output[1].Status.Should().Be(Status.Completed);

            repositoryMock.Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = nameof(ShouldReturnEmptyListWhenNoTasksFound))]
        public async Task ShouldReturnEmptyListWhenNoTasksFound()
        {
            var repositoryMock = new Mock<ITaskRepository>();
            var useCase = new GetAllTasksUseCase(repositoryMock.Object);

            repositoryMock
                .Setup(x =>
                    x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<TaskEntity>());

            var output = await useCase.Handle(1, 10, CancellationToken.None);

            output.Should().NotBeNull();
            output.Count.Should().Be(0);
            repositoryMock.Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
