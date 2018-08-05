using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DreamsMadeTrue.Core.Models
{
    public class MongoObject : IMongoObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
