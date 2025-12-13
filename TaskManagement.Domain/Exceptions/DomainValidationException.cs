namespace TaskManagement.Domain.Exception
{
    public class DomainValidationException : System.Exception
    {
        public DomainValidationException(string message) : base(message) { }
    }
}
