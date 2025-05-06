using FluentValidation;
using Stream.Service.BusinessLogic.Contracts;

namespace Stream.Service.BusinessLogic.Validators;

public class ChangeActiveDonationGoalDtoValidator : AbstractValidator<ChangeActiveDonationGoalDto>
{
    public ChangeActiveDonationGoalDtoValidator()
    {
        RuleFor(dto => dto.Title)
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.")
            .When(dto => !string.IsNullOrWhiteSpace(dto.Title));
        
        RuleFor(dto => dto.TargetAmount)
            .GreaterThan(0).WithMessage("TargetAmount must be greater than 0.")
            .When(dto => dto.TargetAmount.HasValue);
    }
}