using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Entities;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.UserUseCases;

public class FollowUserUseCase : IFollowUserUseCase
{
    private readonly IUserRepository _userRepository;

    public FollowUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task ExecuteAsync(Guid followerId, Guid followeeId, CancellationToken cancellationToken)
    {
        if(!await _userRepository.IsExistByIdAsync(followerId, cancellationToken))
            throw new NotFoundException("User is not found");
        if(!await _userRepository.IsExistByIdAsync(followeeId, cancellationToken))
            throw new NotFoundException("User you are going to follow is not found");
        if(await _userRepository.IsFollowingUser(followerId, followeeId, cancellationToken))
        {
            throw new AlreadyExistsException("You are already following this user");
        }
        Follow followModel = new()
        {
            FollowerId = followerId,
            FollowingId = followeeId
        };
        await _userRepository.FollowUser(followModel, cancellationToken);
    }
}