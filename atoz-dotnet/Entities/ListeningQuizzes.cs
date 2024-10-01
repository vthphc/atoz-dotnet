using MongoDB.Bson.Serialization.Attributes;

namespace atoz_dotnet.Entities
{
    public class ListeningQuizzes
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("fullSentence")]
        public string? FullSentence { get; set; }

        [BsonElement("answers")]
        public string[]? Answers { get; set; }

        [BsonElement("publicId")]
        public string? PublicId { get; set; }

        [BsonElement("__v")]
        public int? version { get; set; }
    }
}
