using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.UseCases.Create.Input
{
    public class CreateTaskInput(
        string title,
        string? description,
        DateTime? finishAt,
        Status status)
    {

        //create properties for Title, Description, FinishAt and Status
        public string Title { get; set; } = title;

        public string? Description { get; set; } = description;

        public DateTime? FinishAt { get; set; } = finishAt;

        public Status Status { get; set; } = status;


    }
}
