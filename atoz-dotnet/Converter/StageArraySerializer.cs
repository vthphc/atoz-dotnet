using atoz_dotnet.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace atoz_dotnet.Converter
{
    public class StageArraySerializer : SerializerBase<Stage[]>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Stage[] value)
        {
            context.Writer.WriteStartArray();
            foreach (var stage in value)
            {
                BsonSerializer.Serialize(context.Writer, stage);
            }
            context.Writer.WriteEndArray();
        }

        public override Stage[] Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var stages = new System.Collections.Generic.List<Stage>();
            context.Reader.ReadStartArray();
            while (context.Reader.ReadBsonType() != BsonType.EndOfDocument)
            {
                var stage = BsonSerializer.Deserialize<Stage>(context.Reader);
                stages.Add(stage);
            }
            context.Reader.ReadEndArray();
            return stages.ToArray();
        }
    }
}
