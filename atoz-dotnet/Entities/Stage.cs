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
        //add topic string type
        public string? Topic{ get; set; }
        [BsonElement("topic")]
        public int ClearTime { get; set; }
    }
}
