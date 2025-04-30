namespace Stream.Service.Domain.Models;

public class StreamCategory
{
    public string Id { get; set; } 
    public string Name { get; set; } 
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}