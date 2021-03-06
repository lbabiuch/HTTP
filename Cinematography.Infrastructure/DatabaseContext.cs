using MongoDB.Driver;
using System;

namespace Cinematography.Infrastructure
{
    public class DatabaseContext
    {
        private readonly IMongoDatabase _database;

        public IMongoDatabase Database => _database;

        public DatabaseContext(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("Cinematography");
        }

        internal object GetCollection<T>(string v)
        {
            throw new NotImplementedException();
        }
    }
}
