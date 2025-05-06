using FluentValidation;
using Stream.Service.BusinessLogic.Contracts;

namespace Stream.Service.BusinessLogic.Validators;

public class CreateDonationGoalDtoValidator : AbstractValidator<CreateDonationGoalDto>
{
    public CreateDonationGoalDtoValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(dto => dto.TargetAmount)
            .GreaterThan(0).WithMessage("TargetAmount must be greater than 0.");
    }
}