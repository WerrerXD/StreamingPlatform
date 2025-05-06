using FluentValidation;
using Stream.Service.BusinessLogic.Commands;

namespace Stream.Service.BusinessLogic.Validators;

public class CreateStreamCategoryCommandValidator : AbstractValidator<CreateStreamCategoryCommand>
{
    public CreateStreamCategoryCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        
        RuleFor(command => command.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}