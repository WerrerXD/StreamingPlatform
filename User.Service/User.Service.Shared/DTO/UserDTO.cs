namespace User.Service.Shared.DTO;

public record UserDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime BlockedUntil { get; set; }
    public string Description { get; set; }
    public string AvatarUrl { get; set; }
    public int FollowersCount { get; set; }
    public int FollowingCount { get; set; }
}