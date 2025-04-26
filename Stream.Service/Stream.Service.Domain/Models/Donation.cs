using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stream.Service.Domain.Models;

public class Donation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 
    
    public string StreamId { get; set; } 
    public string DonorId { get; set; } 
    public string DonorName { get; set; } 
    public decimal Amount { get; set; } 
    public string Message { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
}