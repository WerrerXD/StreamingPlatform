using Microsoft.AspNetCore.Http;
using User.Service.Application.Contracts;

namespace User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;

public interface IChangeUserProfile
{
    Task ExecuteAsync(Guid userId, ChangeUserProfileRequest request, CancellationToken cancellationToken);
}