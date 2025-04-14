using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stream.Service.Domain.Models;

public class StreamModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string StreamerId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime? EndTime { get; set; }
    public int ViewersCount { get; set; } = 0;
}