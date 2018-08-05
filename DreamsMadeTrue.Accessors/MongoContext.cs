using MongoDB.Driver;

namespace DreamsMadeTrue.Accessors
{
    public class MongoContext
    {
        public readonly MongoClient _client;
        public readonly IMongoDatabase _database;

        public MongoContext(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }
    }
}
