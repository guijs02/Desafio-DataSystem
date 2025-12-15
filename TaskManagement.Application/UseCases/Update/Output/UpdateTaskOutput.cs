using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.UseCases.Update.Output
{
    public class UpdateTaskOutput(int id, string title, string? description, DateTime? finishAt, Status status)
    {
        public int Id { get; set; } = id;
        public string Title { get; set; } = title;
        public string? Description { get; set; } = description;
        public DateTime? FinishAt { get; set; } = finishAt;
        public Status Status { get; set; } = status;
    }
}
