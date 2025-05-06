using Stream.Service.BusinessLogic.Contracts;

namespace Stream.Service.BusinessLogic.Validators;

using FluentValidation;

public class DonateStreamerDtoValidator : AbstractValidator<DonateStreamerDto>
{
    public DonateStreamerDtoValidator()
    {
        RuleFor(dto => dto.DonorId)
            .NotEmpty().WithMessage("DonorId is required.");
        //TODO after connect with UserService .Must(BeAValidGuid).WithMessage("DonorId must be a valid GUID.");
        
        RuleFor(dto => dto.DonorName)
            .NotEmpty().WithMessage("DonorName is required.")
            .MaximumLength(100).WithMessage("DonorName must not exceed 100 characters.");

        RuleFor(dto => dto.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");
        
        RuleFor(dto => dto.Message)
            .MaximumLength(500).WithMessage("Message must not exceed 500 characters.");
    }
}