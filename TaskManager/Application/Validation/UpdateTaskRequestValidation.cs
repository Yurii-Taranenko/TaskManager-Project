using FluentValidation;
using TaskManager.Application.DTO;

namespace TaskManager.Application.Validation
{
    public class UpdateTaskRequestValidation : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskRequestValidation() 
        {
            RuleFor(x => x.Title)
                .MaximumLength(100).WithMessage("Title must be less than 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Title));

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Maximum length is 1000")
                .When(x => !string.IsNullOrEmpty(x.Title));

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.")
                .When(x => x.DueDate.HasValue);
            
            RuleFor(x=>x.Priority)
                .IsInEnum().WithMessage("Invalid priority value.")
                .When(x=> x.Priority.HasValue);
        }
    }
}
