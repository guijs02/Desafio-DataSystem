using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.UseCases.Delete;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Repository;

namespace TaskManagement.UnitTests.Application
{
    public class DeleteTaskTest
    {
        //should create tests for DeleteTask use case
        [Fact(DisplayName = nameof(ShouldDeleteTaskFromRepository))]
        public async System.Threading.Tasks.Task ShouldDeleteTaskFromRepository()
        {
            // Arrange
            var repositoryMock = new Mock<ITaskRepository>();
            var task = new TaskManagement.Domain.Entity.Task
            (
                "Test Task",
                "This is a test task",
                Status.Pending
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
