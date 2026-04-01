using FluentValidation;
using TaskManager.Application.DTO;

namespace TaskManager.Application.Validation
{
    public class CreateTaskRequestValidation : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidation() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must be less than 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Maximum length is 1000")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.")
                .When(x => x.DueDate.HasValue);
            
            RuleFor(x=>x.Priority)
                .IsInEnum().WithMessage("Invalid priority value.")
                .When(x=> x.Priority.HasValue);
        }
    }
}
