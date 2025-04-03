using Microsoft.AspNetCore.Http;

namespace User.Service.Application.UseCases.UserUseCases;

public interface IChangeUserProfile
{
    Task ExecuteAsync(Guid userId, string? userName, string? description, IFormFile? avatarPhoto, CancellationToken cancellationToken);
}