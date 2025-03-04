namespace User.Service.Domain.Entities;

public class Report
{
    public Guid Id { get; set; }
    public Guid ReporterId { get; set; }
    public AppUser Reporter { get; set; }
    
    public Guid ReportedId { get; set; }
    public AppUser Reported { get; set; }
    
    public string Reason { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}