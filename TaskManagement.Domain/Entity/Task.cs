using TaskManagement.Domain.Validation;

namespace TaskManagement.Domain.Entity
{
    public class Task
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? FinishAt { get; private set; }
        public Status Status { get; private set; }

        public Task(string title, string? description, Status status, DateTime createdAt, DateTime? finishAt = null)
        {
            Title = title;
            Description = description;
            FinishAt = finishAt;
            Status = status;
            CreatedAt = createdAt;

            Validate();
        }

        public void UpdateTitle(string title)
        {
            Title = title;
            Validate();
        }

        public void UpdateDescription(string? description)
        {
            Description = description;
            Validate();
        }

        public void UpdateStatus(Status status)
        {
            Status = status;
            Validate();
        }

        public void UpdateFinishAt(DateTime? finishAt)
        {
            FinishAt = finishAt;
            Validate();
        }

        public void Validate()
        {
            DomainValidation.TitleIsNullOrWhiteSpace(Title);
            DomainValidation.TitleMaxLength(Title);
            DomainValidation.FinishAtIsEarlierThanCreatedAt(FinishAt, CreatedAt);
            DomainValidation.CreatedAtDefault(CreatedAt);
        }
    }
}
