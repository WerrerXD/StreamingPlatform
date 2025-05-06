using FluentValidation;
using MongoDB.Bson;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Contracts;

namespace Stream.Service.BusinessLogic.Validators;

public class CreateChatMessageCommandValidator : AbstractValidator<CreateChatMessageDto>
{
    public CreateChatMessageCommandValidator()
    {
        RuleFor(dto => dto.UserId)
            .NotEmpty().WithMessage("UserId is required."); 
            //TODO after connect with UserService .Must(BeAValidGuid).WithMessage("UserId must be a valid GUID.");
        
        RuleFor(dto => dto.Message)
            .NotEmpty().WithMessage("Message is required.")
            .MaximumLength(500).WithMessage("Message must not exceed 500 characters.");
    }
    
    private bool BeAValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}