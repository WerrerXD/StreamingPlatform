using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stream.Service.Domain.Models;

public class ChatMessage
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string StreamId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}