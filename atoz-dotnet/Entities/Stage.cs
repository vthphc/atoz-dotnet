using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace atoz_dotnet.Entities
{
    public class Stage
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("star")]
        public int Star { get; set; }
        [BsonElement("clearTime")]
        public int ClearTime { get; set; }
    }
}
