using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Domain.Exceptions
{
    public class TaskNotFoundException : System.Exception
    {
        public TaskNotFoundException(string message) : base(message) { }
    }
}
