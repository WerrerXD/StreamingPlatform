using FluentValidation;
using MongoDB.Bson;
using Stream.Service.BusinessLogic.Commands;

namespace Stream.Service.BusinessLogic.Validators;

public class ChangeStreamCategoryCommandValidator : AbstractValidator<ChangeStreamCategoryCommand>
{
    public ChangeStreamCategoryCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(BeAValidObjectId).WithMessage("Id must be a valid MongoDB ObjectId.");
        
        RuleFor(command => command.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
            .When(command => !string.IsNullOrEmpty(command.Name));
        
        RuleFor(command => command.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
            .When(command => !string.IsNullOrEmpty(command.Description));
    }
    
    private bool BeAValidObjectId(string id)
    {
        return ObjectId.TryParse(id, out _);
    }
}