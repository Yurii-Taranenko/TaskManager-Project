using FluentValidation;
using TaskManager.Application.DTO;

namespace TaskManager.Application.Validation
{
    public class CreateTaskRequestValidation : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidation() 
        {
            RuleFor(x => x.Title)
                .MaximumLength(100).WithMessage("Title must be less than 100 characters.")
                .NotEmpty().WithMessage("Title is required.")
                .NotNull().WithMessage("Title is required.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Maximum length is 1000");
        }
    }
}
