using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.UseCases.GetAll
{
    public class GetAllTasksOutput
    {
        //should have properties Id, Title, Description, Status, CreatedAt, FinishAt and pagination info
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishAt { get; set; }
        public Status Status { get; set; }
        public int? TotalCount { get; set; }
        public int? PageCount { get; set; }
        public int? PageSize { get; set; }


    }
}
