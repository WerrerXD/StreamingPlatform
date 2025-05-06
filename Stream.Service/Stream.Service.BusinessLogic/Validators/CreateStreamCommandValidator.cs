using FluentValidation;
using MongoDB.Bson;
using Stream.Service.BusinessLogic.Commands;

namespace Stream.Service.BusinessLogic.Validators;

public class CreateStreamCommandValidator : AbstractValidator<CreateStreamCommand>
{
    public CreateStreamCommandValidator()
    {
        RuleFor(command => command.StreamerId)
            .NotEmpty().WithMessage("StreamerId is required.");
        //TODO after connect with UserService .Must(BeAValidGuid).WithMessage("SreamerId must be a valid GUID.");
        
        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
        
        RuleFor(command => command.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        
        RuleFor(command => command.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.")
            .Must(BeAValidObjectId).WithMessage("Id must be a valid MongoDB ObjectId.");
    }
    
    private bool BeAValidObjectId(string id)
    {
        return ObjectId.TryParse(id, out _);
    }
}