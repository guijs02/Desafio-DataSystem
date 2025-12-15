namespace TaskManagement.Domain.Exception
{
    public class DomainValidationException(string message) : System.Exception(message)
    {
    }
}
