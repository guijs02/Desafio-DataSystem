using TaskManagement.Domain.Exception;

namespace TaskManagement.Domain.Validation
{
    public class DomainValidation
    {
        public static void CreatedAtDefault(DateTime createdAt)
        {
            if (createdAt == default)
            {
                throw new DomainValidationException("CreatedAt must be set to the current date and time.");
            }
        }

        public static void FinishAtIsEarlierThanCreatedAt(DateTime? finishAt, DateTime createdAt)
        {
            if (finishAt.HasValue && finishAt < createdAt)
            {
                throw new DomainValidationException("Finish date cannot be earlier than creation date.");
            }
        }

        public static void TitleMaxLength(string title)
        {
            if (title.Length > 100)
            {
                throw new DomainValidationException("Title cannot exceed 100 characters.");
            }
        }

        public static void TitleIsNullOrWhiteSpace(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new DomainValidationException("Title cannot be empty or null.");
            }
        }
    }
}
