namespace TaskManagement.Domain.Exceptions
{
    public class TaskNotFoundException(string message) : System.Exception(message)
    {
    }
}
