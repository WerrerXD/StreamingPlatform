using Microsoft.AspNetCore.Http;

namespace User.Service.Shared.DTO;

public record ChangeUserProfileRequest
{
    public string? Username { get; init; }
    public string? Description { get; init; }
    public IFormFile? AvatarPhoto { get; init; }
}