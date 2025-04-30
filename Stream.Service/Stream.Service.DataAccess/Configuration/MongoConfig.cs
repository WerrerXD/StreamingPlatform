using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Stream.Service.Domain.Models;

namespace Stream.Service.DataAccess.Configuration;

public static class MongoConfig
{
    public static void Configure()
    {
        RegisterClassWithObjectId<ChatMessage>();
        RegisterClassWithObjectId<Donation>();
        RegisterClassWithObjectId<DonationGoal>();
        RegisterClassWithObjectId<StreamCategory>();
        RegisterClassWithObjectId<StreamModel>();
    }

    private static void RegisterClassWithObjectId<T>() where T : class
    {
        BsonClassMap.RegisterClassMap<T>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
            
            map.MapIdProperty("Id")
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(MongoDB.Bson.BsonType.ObjectId));
        });
    }
}