using FluentValidation;
using TaskManager.Core.DTOs.Project;

namespace TaskManager.API.Validators.Project
{
    public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
    {
        public CreateProjectRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name field is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters")
                .MaximumLength(100).WithMessage("Name must not be greater than 3 characters");
        }
    }
}
