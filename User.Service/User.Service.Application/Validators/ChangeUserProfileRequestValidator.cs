using FluentValidation;
using Microsoft.AspNetCore.Http;
using User.Service.Application.Contracts;

namespace User.Service.Application.Validators;

public class ChangeUserProfileRequestValidator : AbstractValidator<ChangeUserProfileRequest>
{
    public ChangeUserProfileRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required when provided.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.Username));
        
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.AvatarPhoto)
            .Must(BeAValidImageFile).WithMessage("Avatar must be a valid image file (JPEG or PNG).")
            .When(x => x.AvatarPhoto != null);

        RuleFor(x => x.AvatarPhoto)
            .Must(file => file == null || file.Length <= 5 * 1024 * 1024) // 5 MB
            .WithMessage("Avatar file size must not exceed 5 MB.")
            .When(x => x.AvatarPhoto != null);
    }

    private bool BeAValidImageFile(IFormFile? file)
    {
        if (file == null)
        {
            return true;
        }

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var fileExtension = Path.GetExtension(file.FileName)?.ToLowerInvariant();

        return !string.IsNullOrEmpty(fileExtension) && allowedExtensions.Contains(fileExtension);
    }
}