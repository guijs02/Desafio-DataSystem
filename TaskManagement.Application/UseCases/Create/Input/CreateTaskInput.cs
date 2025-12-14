using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.UseCases.Create.Input
{
    public class CreateTaskInput(
        string title,
        string? description,
        DateTime? finishAt,
        DateTime createdAt,
        Status status)
    {

        //create properties for Title, Description, FinishAt and Status
        public string Title { get; set; } = title;

        public string? Description { get; set; } = description;

        public DateTime? FinishAt { get; set; } = finishAt;

        public DateTime CreatedAt { get; set; } = createdAt;
        public Status Status { get; set; } = status;
    }
}
