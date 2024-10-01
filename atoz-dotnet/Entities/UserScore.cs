using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace atoz_dotnet.Entities
{
    public class UserScore
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("userId"), BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }
        [BsonElement("score"), BsonRepresentation(BsonType.Int32)]
        public int Score { get; set; }
        [BsonElement("date"), BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }
        [BsonElement("__v"), BsonRepresentation(BsonType.Int32)]
        public int Version { get; set; }
    }
}
