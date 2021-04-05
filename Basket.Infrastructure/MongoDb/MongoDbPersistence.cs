using System;
using Basket.Domain.Entities;
using Basket.Domain.Entities.Abstract;
using Basket.Domain.Entities.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Basket.Infrastructure.MongoDb
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoDbPersistence
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Entity>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.Id));
                cm.MapMember(x => x.Id).SetDefaultValue(ObjectId.GenerateNewId());
                cm.IdMemberMap.SetIdGenerator(StringObjectIdGenerator.Instance).SetOrder(1)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(x => x.Id).SetDefaultValue(new ObjectId().ToString);
                cm.MapMember(x => x.CreatedAt).SetDefaultValue(DateTime.UtcNow)
                    .SetSerializer(new DateTimeSerializer(BsonType.DateTime));
                cm.MapMember(x => x.CreatedAt).SetSerializer(new DateTimeSerializer(BsonType.DateTime));
            });

            BsonClassMap.RegisterClassMap<BasketCard>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<BasketItem>(cm =>
            {
                cm.AutoMap();
            });
            
        }
    }
}