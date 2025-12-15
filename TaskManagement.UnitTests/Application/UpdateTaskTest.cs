using FluentAssertions;
using Moq;
using TaskManagement.Application.UseCases.Update;
using TaskManagement.Application.UseCases.Update.Input;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Exception;
using TaskManagement.Domain.Repository;
using Task = System.Threading.Tasks.Task;
using TaskEntity = TaskManagement.Domain.Entity.Task;


namespace TaskManagement.UnitTests.Application
{
    public class UpdateTaskTest
    {

        [Fact(DisplayName = nameof(UpdateTaskWithSuccess))]
        public async Task UpdateTaskWithSuccess()
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

            repository.Setup(r => r.GetByIdAsync(updateTaskInput.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new TaskEntity(
                    "Old Task Title",
                    "Old Task Description",
                    Status.Pending,
                    DateTime.UtcNow.AddDays(3)
                ));

            var output = await useCase.Handle(updateTaskInput, CancellationToken.None);

            repository.Verify(r => r.UpdateAsync(It.IsAny<TaskEntity>(), It.IsAny<CancellationToken>()), Times.Once);

            output.Should().NotBeNull();
            output.Title.Should().Be(updateTaskInput.Title);
            output.Description.Should().Be(updateTaskInput.Description);
            output.FinishAt.Should().Be(updateTaskInput.FinishAt);

        }

        [Fact(DisplayName = nameof(ShouldThrowErrorWhenTitleIsEmpty))]
        public async Task ShouldThrowErrorWhenTitleIsEmpty()
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

            repository.Setup(r => r.GetByIdAsync(updateTaskInput.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new TaskEntity(
                    "Old Task Title",
                    "Old Task Description",
                    Status.Pending,
                    DateTime.UtcNow.AddDays(3)
                ));

            var act = async () => await useCase.Handle(updateTaskInput, CancellationToken.None);

            repository.Verify(r => r.UpdateAsync(It.IsAny<TaskEntity>(), It.IsAny<CancellationToken>()), Times.Never);

            await act.Should().ThrowAsync<DomainValidationException>();
        }
    }
}
