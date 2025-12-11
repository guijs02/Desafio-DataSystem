using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.UseCases.Delete;
using TaskManagement.Domain.Repository;

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
            var taskId = 1;
            var useCase = new DeleteTaskUseCase(repositoryMock.Object);
            // Act
            await useCase.Handle(taskId, CancellationToken.None);
            // Assert
            repositoryMock.Verify(x => x.DeleteAsync(taskId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
