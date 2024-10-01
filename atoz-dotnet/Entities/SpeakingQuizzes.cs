using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace atoz_dotnet.Entities
{
    public class SpeakingQuizzes
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("sentence")]
        public string? Sentence { get; set; }

        [BsonElement("__v")]
        public int? version { get; set; }
    }
}
