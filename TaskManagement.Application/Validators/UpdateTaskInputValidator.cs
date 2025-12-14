using FluentValidation;
using TaskManagement.Application.UseCases.Update.Input;

namespace TaskManagement.Application.Validators
{
    public class UpdateTaskInputValidator : AbstractValidator<UpdateTaskInput>
    {
        public UpdateTaskInputValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty or null.")
                .NotNull().WithMessage("Title cannot be empty or null.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        }
    }
}
