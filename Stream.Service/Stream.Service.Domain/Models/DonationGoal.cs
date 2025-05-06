namespace Stream.Service.Domain.Models;

public class DonationGoal
{
    public string Id { get; set; }
    public string StreamerId { get; set; }
    public string Title { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CollectedAmount { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ClosedAt { get; set; }
}