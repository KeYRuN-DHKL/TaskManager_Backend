using FluentValidation;
using TaskManager.Core.DTOs.AppTask;

namespace TaskManager.API.Validators.AppTask
{
    public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is Required")
                .MaximumLength(200).WithMessage("The maximum length is 200 characters");

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Invalid Property Value");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("The Due date Must be in future");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("Enter the valid Project Id");

            RuleFor(x => x.AssigneeId)
                .GreaterThan(0).WithMessage("Enter the valid Assignee Id");
        }
    }
}
