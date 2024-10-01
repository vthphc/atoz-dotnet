using MongoDB.Driver;

namespace atoz_dotnet.Database
{
    public class MongoDbService
    {
        private readonly IConfiguration configuration;
        private readonly IMongoDatabase? database;

        public MongoDbService(IConfiguration configuration)
        {
            this.configuration = configuration;

            MongoDbConfig.RegisterClassMaps();

            var connectionString = configuration.GetConnectionString("DbConnection");
            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase? GetDatabase()
        {
            return database;
        }
    }
}
