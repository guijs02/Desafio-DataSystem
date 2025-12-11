using FluentAssertions;
using Moq;
using TaskManagement.Application.UseCases.Create;
using TaskManagement.Application.UseCases.Create.Input;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Exception;
using TaskManagement.Domain.Repository;

namespace TaskManagement.UnitTests.Application
{
    public class CreateTaskTest
    {
        //should create a task with success here
        [Fact(DisplayName = nameof(CreateTaskWithSuccess))]
        public async System.Threading.Tasks.Task CreateTaskWithSuccess()
        {
            var repository = new Mock<ITaskRepository>();

            var useCase = new CreateTaskUseCase(repository.Object);

            var entity = new
            {
                Title = "Test Task",
                Description = "This is a test task.",
                FinishAt = DateTime.UtcNow.AddDays(1),
                Status = Status.Pending
            };

            var task = await useCase.Handle(new CreateTaskInput(entity.Title, entity.Description, entity.FinishAt, entity.Status), CancellationToken.None);

            task.Should().NotBeNull();
            task.Title.Should().Be(entity.Title);
            task.Description.Should().Be(entity.Description);
            task.FinishAt.Should().Be(entity.FinishAt);
            task.Status.Should().Be(entity.Status);

        }

        //should thorw error when title is empty
        [Theory(DisplayName = nameof(ShouldThrowErrorWhenTitleIsEmpty))]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public async System.Threading.Tasks.Task ShouldThrowErrorWhenTitleIsEmpty(string? title)
        {
            var repository = new Mock<ITaskRepository>();
            var useCase = new CreateTaskUseCase(repository.Object);
            var entity = new
            {
                Title = title,
                Description = "This is a test task.",
                FinishAt = DateTime.Now.AddDays(1),
            };

            await Assert.ThrowsAsync<DomainValidationException>(async () =>
            {
                await useCase.Handle(new CreateTaskInput(entity.Title!, entity.Description, entity.FinishAt, Status.Pending), CancellationToken.None);
            });
        }

        //should thorw error when title exceeds 100 characters
        [Fact(DisplayName = nameof(ShouldThrowErrorWhenTitleExceeds100Characters))]
        public async System.Threading.Tasks.Task ShouldThrowErrorWhenTitleExceeds100Characters()
        {
            var repository = new Mock<ITaskRepository>();
            var useCase = new CreateTaskUseCase(repository.Object);
            var longTitle = new string('A', 101);
            var entity = new
            {
                Title = longTitle,
                Description = "This is a test task.",
                FinishAt = DateTime.UtcNow.AddDays(1),
            };
            await Assert.ThrowsAsync<DomainValidationException>(async () =>
            {
                await useCase.Handle(new CreateTaskInput(entity.Title, entity.Description, entity.FinishAt, Status.Pending), CancellationToken.None);
            });

        }
    }
}