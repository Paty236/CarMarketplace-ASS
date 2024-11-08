using MongoDB.Driver;
using CarMarketplace.Application.Interfaces;

namespace CarMarketplace.Infrastructure
{
    public class MongoCollectionFactory : IMongoCollectionFactory
    {
        private readonly MongoClient _mongoClient;

        public MongoCollectionFactory(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var database = _mongoClient.GetDatabase("CarMarketplace");
            return database.GetCollection<T>(collectionName);
        }
    }
}
