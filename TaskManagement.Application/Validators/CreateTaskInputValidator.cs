using FluentValidation;
using TaskManagement.Application.UseCases.Create.Input;

namespace TaskManagement.Application.Validators
{
    public class CreateTaskInputValidator : AbstractValidator<CreateTaskInput>
    {
        public CreateTaskInputValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty or null.")
                .NotNull().WithMessage("Title cannot be empty or null.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.FinishAt)
                .Must(finishAt => !finishAt.HasValue || finishAt.Value >= DateTime.Now)
                .WithMessage("Finish date cannot be earlier than creation date.");
        }
    }
}
