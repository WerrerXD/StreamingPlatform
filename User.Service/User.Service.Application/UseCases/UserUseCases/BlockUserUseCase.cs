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
        if(!await _userRepository.IsExistByIdAsync(userId, cancellationToken))
            throw new NotFoundException("User you are going to block is not found");
        
        await _userRepository.BanUserUntil(userId, daysBanned, cancellationToken);
        
        await _userRepository.Save(cancellationToken);
    }
}