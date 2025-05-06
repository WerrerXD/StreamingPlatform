namespace Stream.Service.Domain.Models;

public class StreamModel
{
    public string Id { get; set; } 
    public string StreamerId { get; set; } 
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string CategoryId { get; set; } 
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime? EndTime { get; set; }
    public int ViewersCount { get; set; } = 0;
}