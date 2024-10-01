using atoz_dotnet.Converter;
using MongoDB.Bson.Serialization;

public static class MongoDbConfig
{
    private static bool _isRegistered = false;

    public static void RegisterClassMaps()
    {
        if (!_isRegistered)
        {
            BsonSerializer.RegisterSerializer(new StageSerializer());

            _isRegistered = true;
        }
    }
}
