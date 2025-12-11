using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.UseCases.Get
{
    public class GetByIdOutput(int id, string title, 
        string? description, DateTime createdAt, DateTime? finishAt, Status status)
    {
        public int Id { get; set; } = id;
        public string Title { get; set; } = title;
        public string? Description { get; set; } = description;
        public DateTime CreatedAt { get; set; } = createdAt;
        public DateTime? FinishAt { get; set; } = finishAt;

        public Status Status { get; set; } = status;
    }
}
