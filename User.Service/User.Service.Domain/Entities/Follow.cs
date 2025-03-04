namespace User.Service.Domain.Entities;

public class Follow
{
    public Guid FollowerId { get; set; }
    public AppUser Follower { get; set; }

    public Guid FollowingId { get; set; }
    public AppUser Following { get; set; }

    public DateTime FollowingSince { get; set; } = DateTime.UtcNow;
}