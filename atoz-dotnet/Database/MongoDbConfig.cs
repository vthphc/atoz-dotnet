using atoz_dotnet.Converter;
using atoz_dotnet.Entities;
using MongoDB.Bson.Serialization;

public static class MongoDbConfig
{
    private static bool _isRegistered = false;

    public static void RegisterClassMaps()
    {
        if (!_isRegistered)
        {
            BsonSerializer.RegisterSerializer(new StageSerializer());
            BsonSerializer.RegisterSerializer(new StageArraySerializer());

            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

            _isRegistered = true;
        }
    }
}
