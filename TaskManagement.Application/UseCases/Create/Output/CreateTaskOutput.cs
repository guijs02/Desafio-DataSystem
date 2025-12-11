using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.UseCases.Create.Output
{
    public class CreateTaskOutput(string title, 
        string? description, 
        DateTime createdAt, 
        DateTime? finishAt, 
        Status status)
    {
        public string Title { get; set; } = title;

        public string? Description { get; set; } = description;

        public DateTime CreatedAt { get; set; } = createdAt;

        public DateTime? FinishAt { get; set; } = finishAt;

        public Status Status { get; set; } = status;
    }
}
