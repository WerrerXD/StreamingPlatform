using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stream.Service.Domain.Models;

public class ChatMessage
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 

    public string StreamId { get; set; } 
    public string UserId { get; set; } 
    public string Message { get; set; } 
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}