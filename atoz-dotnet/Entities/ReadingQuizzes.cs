using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace atoz_dotnet.Entities
{
    public class ReadingQuizzes
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("paragraphsList")]
        public string[]? ParagraphList { get; set; }

        [BsonElement("questionsList")]
        public string[]? QuestionsList { get; set; }

        [BsonElement("language")]
        public string? Language { get; set; }

        [BsonElement("difficulty")]
        public int? Difficulty { get; set; }

        [BsonElement("__v")]
        public int? version { get; set; }
    }
}
