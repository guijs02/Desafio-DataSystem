using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.UseCases.Update.Input
{
    public class UpdateTaskInput(int id, string title, string? description, DateTime? finishAt, Status status)
    {

        //create properties for Title, Description, FinishAt and Status
        public int Id { get; set; } = id;
        public string Title { get; set; } = title;

        public string? Description { get; set; } = description;

        public DateTime? FinishAt { get; set; } = finishAt;

        public Status Status { get; set; } = status;
    }
}
