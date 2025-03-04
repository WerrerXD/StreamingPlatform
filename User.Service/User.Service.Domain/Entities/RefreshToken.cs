namespace User.Service.Domain.Entities;

public class RefreshToken
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; }
    
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}