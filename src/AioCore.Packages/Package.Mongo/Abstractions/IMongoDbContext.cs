using MongoDB.Driver;

namespace Package.Mongo.Abstractions
{
    public interface IMongoDbContext
    {
        IMongoClient Client { get; }

        IMongoDatabase Database { get; }

        IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey = null);

        void DropCollection<TDocument>(string partitionKey = null);

        void SetGuidRepresentation(MongoDB.Bson.GuidRepresentation guidRepresentation);
    }
}