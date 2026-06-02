using FluentValidation;
using TaskManager.Core.DTOs.AppTask;

namespace TaskManager.API.Validators.AppTask
{
    public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is Required")
                .MaximumLength(200);

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Enter the valid value for the status");

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Enter the valid value for the Priority");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Due Date must be in the future");

            RuleFor(x => x.AssigneeId)
                .GreaterThan(0).WithMessage("The Assignee Id must be greater than 0");
        }
    }
}
