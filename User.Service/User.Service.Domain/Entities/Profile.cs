namespace User.Service.Domain.Entities;

public class Profile
{
    public Guid UserId { get; set; }
    public string ProfileName { get; set; }
    public string Description { get; set; }
    public string AvatarUrl { get; set; }
    public int FollowersCount { get; set; } = 0;
    public int FollowingCount { get; set; } = 0;
    public AppUser User { get; set; }
}