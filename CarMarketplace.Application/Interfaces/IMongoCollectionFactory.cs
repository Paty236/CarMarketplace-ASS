using MongoDB.Driver;

namespace CarMarketplace.Application.Interfaces
{
    public interface IMongoCollectionFactory
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
