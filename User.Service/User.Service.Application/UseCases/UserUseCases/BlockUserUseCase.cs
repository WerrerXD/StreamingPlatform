using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.UserUseCases;

public class BlockUserUseCase : IBlockUserUseCase
{
    private readonly IUserRepository _userRepository;

    public BlockUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task ExecuteAsync(Guid userId, int daysBanned, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken) 
            ?? throw new NotFoundException("User you are going to block is not found");
        
        user.IsBlocked = true;
        user.BlockedUntil = DateTime.UtcNow.AddDays(daysBanned);
        
        await _userRepository.UpdateAsync(user, cancellationToken);
    }
}