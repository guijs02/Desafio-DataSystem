using TaskManagement.Domain.Exception;

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

        public Task(string title, string? description, Status status, DateTime? finishAt = null)
        {
            Title = title;
            Description = description;
            FinishAt = finishAt;
            Status = status;
            CreatedAt = DateTime.Now;

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

        //validate methods can be added here
        public void Validate()
        {
            // Example validation logic
            if (string.IsNullOrWhiteSpace(Title))
            {
                throw new DomainValidationException("Title cannot be empty or null.");
            }

            if(Title.Length > 100)
            {
                throw new DomainValidationException("Title cannot exceed 100 characters.");
            }

            if (FinishAt.HasValue && FinishAt < CreatedAt)
            {
                throw new DomainValidationException("Finish date cannot be earlier than creation date.");
            }

            if (CreatedAt == default)
            {
                throw new DomainValidationException("CreatedAt must be set to the current date and time.");
            }
        }

    }
    public enum Status
    {
        Pending,
        InProgress,
        Completed,
    }
}
