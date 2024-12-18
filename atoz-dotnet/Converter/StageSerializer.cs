using atoz_dotnet.Entities;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace atoz_dotnet.Converter
{
    public class StageSerializer : IBsonSerializer<Stage>
    {
        public Type ValueType => typeof(Stage);

        public Stage Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;

            bsonReader.ReadStartDocument();
            string? id = null;
            int star = 0;
            int clearTime = 0;

            while (bsonReader.ReadBsonType() != BsonType.EndOfDocument)
            {
                var name = bsonReader.ReadName();
                switch (name)
                {
                    case "_id":
                        if (bsonReader.CurrentBsonType == BsonType.ObjectId)
                        {
                            id = bsonReader.ReadObjectId().ToString();
                        }
                        else if (bsonReader.CurrentBsonType == BsonType.String)
                        {
                            id = bsonReader.ReadString();
                        }
                        break;
                    case "star":
                        star = bsonReader.ReadInt32();
                        break;
                    case "clearTime":
                        clearTime = bsonReader.ReadInt32();
                        break;
                    default:
                        bsonReader.SkipValue();
                        break;
                }
            }
            bsonReader.ReadEndDocument();

            return new Stage
            {
                Id = id,
                Star = star,
                ClearTime = clearTime
            };
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
        //add Serialize for array of Stage
        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Stage value)
        {
            var bsonWriter = context.Writer;

            bsonWriter.WriteStartDocument();
            bsonWriter.WriteName("_id");
            bsonWriter.WriteString(value.Id);
            bsonWriter.WriteName("star");
            bsonWriter.WriteInt32(value.Star);
            bsonWriter.WriteName("clearTime");
            bsonWriter.WriteInt32(value.ClearTime);
            bsonWriter.WriteEndDocument();
        }

        void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            Serialize(context, args, (Stage)value);
        }
    }
}
