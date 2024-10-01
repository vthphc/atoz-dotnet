using MongoDB.Bson.Serialization.Attributes;

namespace atoz_dotnet.Entities
{
    public class Book
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("quantity")]
        public int? Quantity { get; set; }

        [BsonElement("bookType")]
        public string? BookType { get; set; }

        [BsonElement("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("__v")]
        public int? Version { get; set; }
    }
}
