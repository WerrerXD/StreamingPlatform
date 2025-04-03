using User.Service.Domain.Entities;

namespace User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;

public interface IGetAllUsersUseCase
{
    Task<List<AppUser>> ExecuteAsync(CancellationToken cancellationToken);
}