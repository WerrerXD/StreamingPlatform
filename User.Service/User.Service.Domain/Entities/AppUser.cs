using System;

namespace User.Service.Domain.Entities;
public class AppUser
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public List<Role> Roles { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsBlocked { get; set; } = false;
    public DateTime BlockedUntil { get; set; } = DateTime.UtcNow.AddDays(-1);

    public string Description { get; set; } = "";
    public string AvatarUrl { get; set; } = "";
    public int FollowersCount { get; set; } = 0;
    public int FollowingCount { get; set; } = 0;
}