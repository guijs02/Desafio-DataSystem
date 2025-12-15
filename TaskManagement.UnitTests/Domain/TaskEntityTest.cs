using FluentAssertions;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Exception;
using Task = TaskManagement.Domain.Entity.Task;
namespace TaskManagement.UnitTests.Domain
{
    public class TaskEntityTest
    {
        [Fact(DisplayName = nameof(InstantiateEntityOk))]
        public void InstantiateEntityOk()
        {
            var entity = new
            {
                Title = "Test Task",
                Description = "This is a test task.",
                FinishAt = DateTime.UtcNow.AddDays(1),
                Status = Status.Pending
            };

            var taskEntity = new Task(
                entity.Title,
                entity.Description,
                entity.Status,
                DateTime.UtcNow
            );

            taskEntity.Title.Should().Be(entity.Title);
            taskEntity.Description.Should().Be(entity.Description);
            taskEntity.CreatedAt.Should().NotBe(null);
            taskEntity.CreatedAt.Should().NotBe(default(DateTime));

        }

        [Fact(DisplayName = nameof(ShouldUpdateTitleOk))]
        public void ShouldUpdateTitleOk()
        {
            var taskEntity = new Task(
                "Initial Title",
                "This is a test task.",
                Status.Pending,
                DateTime.UtcNow
            );
            var newTitle = "Updated Title";
            taskEntity.UpdateTitle(newTitle);
            taskEntity.Title.Should().Be(newTitle);
        }

        [Fact(DisplayName = nameof(ShouldUpdateDescriptionOk))]
        public void ShouldUpdateDescriptionOk()
        {
            var taskEntity = new Task(
                "Initial Title",
                "This is a test task.",
                Status.Pending,
                DateTime.UtcNow
            );
            var newDescription = "Updated Description";
            taskEntity.UpdateDescription(newDescription);
            taskEntity.Description.Should().Be(newDescription);
        }

        [Fact(DisplayName = nameof(ShouldUpdateStatusOk))]
        public void ShouldUpdateStatusOk()
        {
            var taskEntity = new Task(
                "Initial Title",
                "This is a test task.",
                Status.InProgress,
                DateTime.UtcNow
            );
            var newStatus = Status.Completed;
            taskEntity.UpdateStatus(newStatus);
            taskEntity.Status.Should().Be(newStatus);

        }
        
        [Fact(DisplayName = nameof(ShouldUpdateFinishAtOk))]
        public void ShouldUpdateFinishAtOk()
        {
            var taskEntity = new Task(
                "Initial Title",
                "This is a test task.",
                Status.Pending,
                DateTime.UtcNow
            );
            var newFinishAt = DateTime.UtcNow.AddDays(2);
            taskEntity.UpdateFinishAt(newFinishAt);
            taskEntity.FinishAt.Should().Be(newFinishAt);
        }

        [Fact(DisplayName = nameof(ShouldThrowErrorWhenTitleIsEmpty))]
        public void ShouldThrowErrorWhenTitleIsEmpty()
        {
            Action action = () => new Task(
                "",
                "This is a test task.",
                Status.Pending,
                DateTime.UtcNow
            );

            action.Should().Throw<DomainValidationException>()
                .WithMessage("Title cannot be empty or null.");
        }

        [Fact(DisplayName = nameof(ShouldThrowErrorWhenFinishAtIsEarlierThanCreatedAt))]
        public void ShouldThrowErrorWhenFinishAtIsEarlierThanCreatedAt()
        {
            Action action = () => new Task(
                "Test Task",
                "This is a test task.",
                Status.Pending,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(-1)
            );
            action.Should().Throw<DomainValidationException>()
                .WithMessage("Finish date cannot be earlier than creation date.");
        }

        [Fact(DisplayName = nameof(ShouldThrowErrorWhenTitleExceeds100Characters))]
        public void ShouldThrowErrorWhenTitleExceeds100Characters()
        {
            var longTitle = new string('A', 101);
            Action action = () => new Task(
                longTitle,
                "This is a test task.",
                Status.Pending,
                DateTime.UtcNow
            );
            action.Should().Throw<DomainValidationException>()
                .WithMessage("Title cannot exceed 100 characters.");
        }
    }
}
