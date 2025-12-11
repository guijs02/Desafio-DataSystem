using FluentAssertions;
using Moq;
using TaskManagement.Application.UseCases.GetAll;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Repository;

namespace TaskManagement.UnitTests.Application
{
    public class GetAllTaskTest
    {
        //should create tests for GetAllTask use case
        // GetAllTask will use pagination to get all tasks from repository
        [Fact(DisplayName = nameof(ShouldGetAllTasksFromRepository))]
        public async System.Threading.Tasks.Task ShouldGetAllTasksFromRepository()
        {
            // Arrange
            var repositoryMock = new Mock<ITaskRepository>();
            var useCase = new GetAllTasksUseCase(repositoryMock.Object);
            var tasks = new List<TaskManagement.Domain.Entity.Task>
            {
                new("Task 1", "Description 1", Status.Pending),
                new("Task 2", "Description 2", Status.Completed)
            };
            //should setup repository to return a list of tasks when GetAllAsync is called
            repositoryMock
                .Setup(x =>
                x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(tasks);
            // Act
            var output = await useCase.Handle(1, 10, CancellationToken.None);
            // Assert
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

        //should return empty list when no tasks found
        [Fact(DisplayName = nameof(ShouldReturnEmptyListWhenNoTasksFound))]
        public async System.Threading.Tasks.Task ShouldReturnEmptyListWhenNoTasksFound()
        {
            // Arrange
            var repositoryMock = new Mock<ITaskRepository>();
            var useCase = new GetAllTasksUseCase(repositoryMock.Object);
            //should setup repository to return an empty list when GetAllAsync is called
            repositoryMock
                .Setup(x =>
                x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync([]);
            // Act
            var output = await useCase.Handle(1, 10, CancellationToken.None);
            // Assert
            output.Should().NotBeNull();
            output.Count.Should().Be(0);
            repositoryMock.Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
