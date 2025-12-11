using FluentAssertions;
using Moq;
using TaskManagement.Application.UseCases.Update;
using TaskManagement.Application.UseCases.Update.Input;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Exception;
using TaskManagement.Domain.Repository;

namespace TaskManagement.UnitTests.Application
{
    public class UpdateTaskTest
    {
        //should update a task with success here
        [Fact(DisplayName = nameof(UpdateTaskWithSuccess))]
        public async System.Threading.Tasks.Task UpdateTaskWithSuccess()
        {
            var repository = new Mock<ITaskRepository>();

            var useCase = new UpdateTaskUseCase(repository.Object);

            var updateTaskInput = new UpdateTaskInput
            (
                1,
                "Updated Task Title",
                "Updated Task Description",
                DateTime.UtcNow.AddDays(7),
                Status.InProgress
            );

            //should setup the repository to return a task when GetByIdAsync is called
            repository.Setup(r => r.GetByIdAsync(updateTaskInput.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new TaskManagement.Domain.Entity.Task
                (
                    "Old Task Title",
                    "Old Task Description",
                    Status.Pending,
                    DateTime.UtcNow.AddDays(3)
                ));

            var output = await useCase.Handle(updateTaskInput, CancellationToken.None);

            repository.Verify(r => r.UpdateAsync(It.IsAny<TaskManagement.Domain.Entity.Task>(), It.IsAny<CancellationToken>()), Times.Once);

            output.Should().NotBeNull();
            output.Title.Should().Be(updateTaskInput.Title);
            output.Description.Should().Be(updateTaskInput.Description);
            output.FinishAt.Should().Be(updateTaskInput.FinishAt);

        }

        //update task with failed
        [Fact(DisplayName = nameof(UpdateTaskWithFailed))]
        public async System.Threading.Tasks.Task UpdateTaskWithFailed()
        {
            var repository = new Mock<ITaskRepository>();

            var useCase = new UpdateTaskUseCase(repository.Object);

            var updateTaskInput = new UpdateTaskInput
            (
                1,
                "",
                "Updated Task Description",
                DateTime.UtcNow.AddDays(7),
                Status.InProgress
            );

            //should setup the repository to return a task when GetByIdAsync is called
            repository.Setup(r => r.GetByIdAsync(updateTaskInput.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new TaskManagement.Domain.Entity.Task
                (
                    "Old Task Title",
                    "Old Task Description",
                    Status.Pending,
                    DateTime.UtcNow.AddDays(3)
                ));

            var act = async () => await useCase.Handle(updateTaskInput, CancellationToken.None);

            repository.Verify(r => r.UpdateAsync(It.IsAny<TaskManagement.Domain.Entity.Task>(), It.IsAny<CancellationToken>()), Times.Never);

            await Assert.ThrowsAsync<DomainValidationException>(act);

        }
    }
}
