using FluentAssertions;
using Moq;
using TaskManagement.Application.UseCases.Create;
using TaskManagement.Application.UseCases.Create.Input;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Exception;
using TaskManagement.Domain.Repository;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.UnitTests.Application
{
    public class CreateTaskTest
    {
        [Fact(DisplayName = nameof(CreateTaskWithSuccess))]
        public async Task CreateTaskWithSuccess()
        {
            var repository = new Mock<ITaskRepository>();

            var useCase = new CreateTaskUseCase(repository.Object);

            var entity = new
            {
                Title = "Test Task",
                Description = "This is a test task.",
                FinishAt = DateTime.UtcNow.AddDays(1),
                Status = Status.Pending,
                CreatedAt = DateTime.UtcNow,
            };

            var task = await useCase.Handle(
                new CreateTaskInput(
                    entity.Title,
                    entity.Description,
                    entity.FinishAt,
                    entity.CreatedAt,
                    entity.Status
                ),
                CancellationToken.None
            );

            task.Should().NotBeNull();
            task.Title.Should().Be(entity.Title);
            task.Description.Should().Be(entity.Description);
            task.FinishAt.Should().Be(entity.FinishAt);
            task.Status.Should().Be(entity.Status);

        }

        [Theory(DisplayName = nameof(ShouldThrowErrorWhenTitleIsEmpty))]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public async Task ShouldThrowErrorWhenTitleIsEmpty(string? title)
        {
            var repository = new Mock<ITaskRepository>();
            var useCase = new CreateTaskUseCase(repository.Object);
            var entity = new
            {
                Title = title,
                Description = "This is a test task.",
                FinishAt = DateTime.Now.AddDays(1),
                CreatedAt = DateTime.UtcNow,
            };

            var act = async () =>
            {
                await useCase.Handle(
                    new CreateTaskInput(
                        entity.Title!,
                        entity.Description,
                        entity.FinishAt,
                        entity.CreatedAt,
                        Status.Pending
                    ),
                    CancellationToken.None
                );
            };

            await act.Should().ThrowAsync<DomainValidationException>();
        }

        [Fact(DisplayName = nameof(ShouldThrowErrorWhenTitleExceeds100Characters))]
        public async Task ShouldThrowErrorWhenTitleExceeds100Characters()
        {
            var repository = new Mock<ITaskRepository>();
            var useCase = new CreateTaskUseCase(repository.Object);
            var longTitle = new string('A', 101);
            var entity = new
            {
                Title = longTitle,
                Description = "This is a test task.",
                FinishAt = DateTime.UtcNow.AddDays(1),
                CreatedAt = DateTime.UtcNow,
            };

            var act = async () =>
            {
                await useCase.Handle(new CreateTaskInput(
                    entity.Title,
                    entity.Description,
                    entity.FinishAt,
                    entity.CreatedAt,
                    Status.Pending
                ), CancellationToken.None);
            };

            await act.Should().ThrowAsync<DomainValidationException>();
        }
    }
}