using User.Service.Domain.Entities;

namespace User.Service.Domain.Interfaces;

public interface IUserRepository: IRepository<AppUser>
{
    Task<AppUser> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<AppUser> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task AddAdminToUserAsync(Guid userId, CancellationToken cancellationToken);
    Task FollowUser(Follow follow, CancellationToken cancellationToken);
    Task UnfollowUser(Follow follow, CancellationToken cancellationToken);
    Task<bool> IsFollowingUser(Guid followerId, Guid followeeId, CancellationToken cancellationToken);
    Task<bool> IsExistByIdAsync(Guid id, CancellationToken cancellationToken);
    Task BanUserUntil(Guid userId, int daysBanned, CancellationToken cancellationToken);
    Task SetAvatarUrl(Guid userId, string avatarUrl, CancellationToken cancellationToken);
    Task SetUserName(Guid userId, string userName, CancellationToken cancellationToken);
    Task SetDescription(Guid userId, string description, CancellationToken cancellationToken);
    Task<AppUser> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task UnblockExpiredUsers(CancellationToken cancellationToken);

}