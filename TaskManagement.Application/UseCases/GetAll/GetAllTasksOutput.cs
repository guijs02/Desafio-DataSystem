using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.UseCases.GetAll
{
    public class GetAllTasksOutput(
        int id,
        string title,
        string description,
        DateTime createdAt,
        DateTime? finishAt,
        Status status,
        int? totalCount,
        int? pageNumber,
        int? pageSize)
    {
        public int Id { get; set; } = id;
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public DateTime CreatedAt { get; set; } = createdAt;
        public DateTime? FinishAt { get; set; } = finishAt;
        public Status Status { get; set; } = status;
        public int? TotalCount { get; set; } = totalCount;
        public int? PageNumber { get; set; } = pageNumber;
        public int? PageSize { get; set; } = pageSize;
    }
}
