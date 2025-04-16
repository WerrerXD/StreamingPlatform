using User.Service.Domain.Entities;

namespace User.Service.Domain.Interfaces;

public interface IUserRepository: IRepository<AppUser>
{
    Task<AppUser> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<AppUser> GetByIdWithRolesAsync(Guid id, CancellationToken cancellationToken);
    Task<AppUser> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> IsFollowingUserAsync(Guid followerId, Guid followeeId, CancellationToken cancellationToken);
    Task<bool> IsExistByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<AppUser> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<List<AppUser>> GetAllExpiredUsers(CancellationToken cancellationToken);
    Task<Role> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken);
    Task FollowUserAsync(Follow follow, CancellationToken cancellationToken);
    Task UnFollowUserAsync(Follow follow, CancellationToken cancellationToken);
}