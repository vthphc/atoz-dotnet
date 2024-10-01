namespace atoz_dotnet.Configurations
{
    public class EnvironmentVariablesConfigurationProvider : ConfigurationProvider
    {
        private const string MongoDbConnectionStringURL = "MONGODB_CONNECTION_STRING_URL";

        public override void Load()
        {
            Data[MongoDbConnectionStringURL] = Environment.GetEnvironmentVariable(MongoDbConnectionStringURL);
        }
    }
}
