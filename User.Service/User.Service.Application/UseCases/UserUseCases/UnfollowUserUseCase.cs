using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Entities;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.UserUseCases;

public class UnfollowUserUseCase : IUnfollowUserUseCase
{
    private readonly IUserRepository _userRepository;

    public UnfollowUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task ExecuteAsync(Guid followerId, Guid followeeId, CancellationToken cancellationToken)
    {
        var follower = await _userRepository.GetByIdAsync(followerId, cancellationToken) 
            ?? throw new NotFoundException("User is not found");
        
        var followee = await _userRepository.GetByIdAsync(followeeId, cancellationToken)
            ?? throw new NotFoundException("User you are going to unfollow is not found");

        var isFollowing = await _userRepository.IsFollowingUserAsync(followerId, followeeId, cancellationToken);
        
        if(!isFollowing)
        {
            throw new AlreadyExistsException("You are not following this user");
        }
        
        Follow followModel = new()
        {
            FollowerId = followerId,
            FollowingId = followeeId
        };
        
        await _userRepository.UnFollowUserAsync(followModel, cancellationToken);
        
        follower.FollowingCount--;
        followee.FollowersCount--;
        
        await _userRepository.UpdateAsync(follower, cancellationToken);
        await _userRepository.UpdateAsync(followee, cancellationToken);
    }
}