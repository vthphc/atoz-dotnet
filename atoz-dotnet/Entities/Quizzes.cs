using MongoDB.Bson.Serialization.Attributes;

namespace atoz_dotnet.Entities
{
    public class Quizzes
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("question")]
        public string? Question { get; set; }

        [BsonElement("answers")]
        public string[]? Answers { get; set; }

        [BsonElement("correctAnswer")]
        public string? CorrectAnswer { get; set; }

        [BsonElement("quizType")]
        public string? QuizType { get; set; }

        [BsonElement("__v")]
        public int? version { get; set; }
    }
}
