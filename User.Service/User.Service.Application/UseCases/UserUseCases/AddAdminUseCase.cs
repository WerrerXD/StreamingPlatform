using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Entities;
using User.Service.Domain.Enums;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.UserUseCases;

public class AddAdminUseCase : IAddAdminUseCase
{
    private readonly IUserRepository _userRepository;

    public AddAdminUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task ExecuteAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdWithRolesAsync(userId, cancellationToken)
            ?? throw new NotFoundException("User does not exist");
        
        var adminRole = await _userRepository.GetRoleByNameAsync(UserRole.Admin.ToString(), cancellationToken);
        
        user.Roles.Add(adminRole);
        
        await _userRepository.UpdateAsync(user, cancellationToken); 
    }
}