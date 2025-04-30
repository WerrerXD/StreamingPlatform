using FluentValidation;
using MongoDB.Bson;
using Stream.Service.BusinessLogic.Commands;

namespace Stream.Service.BusinessLogic.Validators;

public class ChangeStreamCommandValidator : AbstractValidator<ChangeStreamCommand>
{
    public ChangeStreamCommandValidator()
    {
        RuleFor(command => command.StreamId)
            .NotEmpty().WithMessage("StreamId is required.")
            .Must(BeAValidObjectId).WithMessage("Id must be a valid MongoDB ObjectId.");;
        
        RuleFor(command => command.Dto.StreamName)
            .MaximumLength(100).WithMessage("StreamName must not exceed 100 characters.")
            .When(command => !string.IsNullOrWhiteSpace(command.Dto.StreamName));
        
        RuleFor(command => command.Dto.StreamDescription)
            .MaximumLength(500).WithMessage("StreamDescription must not exceed 500 characters.")
            .When(command => !string.IsNullOrWhiteSpace(command.Dto.StreamDescription));
        
        RuleFor(command => command.Dto.CategoryId)
            .Must(BeAValidObjectId).WithMessage("Id must be a valid MongoDB ObjectId.")
            .When(command => !string.IsNullOrWhiteSpace(command.Dto.CategoryId));
    }
    
    private bool BeAValidObjectId(string id)
    {
        return ObjectId.TryParse(id, out _);
    }
}