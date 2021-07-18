using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Package.Mongo.Abstractions;
using Package.Mongo.Models;

namespace Package.Mongo.DataAccess
{
    public class MongoDbIndexHandler : DataAccessBase
    {
        public MongoDbIndexHandler(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }
        
        public virtual async Task<List<string>> GetIndexesNamesAsync<TDocument, TKey>(string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var indexCursor = await HandlePartitioned<TDocument, TKey>(partitionKey).Indexes.ListAsync();
            var indexes = await indexCursor.ToListAsync();
            return indexes.Select(e => e["name"].ToString()).ToList();
        }
        
        public virtual async Task<string> CreateTextIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey).Indexes
                .CreateOneAsync(
                    new CreateIndexModel<TDocument>(
                        Builders<TDocument>.IndexKeys.Text(field),
                        indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions)
                    ));
        }
        
        public virtual async Task<string> CreateAscendingIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var indexKey = Builders<TDocument>.IndexKeys;
            return await collection.Indexes
                .CreateOneAsync(
                    new CreateIndexModel<TDocument>(indexKey.Ascending(field), createOptions));
        }
        
        public virtual async Task<string> CreateDescendingIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var indexKey = Builders<TDocument>.IndexKeys;
            return await collection.Indexes
                .CreateOneAsync(
                    new CreateIndexModel<TDocument>(indexKey.Descending(field), createOptions));
        }
        
        public virtual async Task<string> CreateHashedIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var indexKey = Builders<TDocument>.IndexKeys;
            return await collection.Indexes
                .CreateOneAsync(
                    new CreateIndexModel<TDocument>(indexKey.Hashed(field), createOptions));
        }
        
        public virtual async Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(IEnumerable<Expression<Func<TDocument, object>>> fields, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var listOfDefs = fields.Select(field => Builders<TDocument>.IndexKeys.Text(field)).ToList();
            return await collection.Indexes
                .CreateOneAsync(new CreateIndexModel<TDocument>(Builders<TDocument>.IndexKeys.Combine(listOfDefs), createOptions));
        }
        
        public virtual async Task DropIndexAsync<TDocument, TKey>(string indexName, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            await HandlePartitioned<TDocument, TKey>(partitionKey).Indexes.DropOneAsync(indexName);
        }
    }
}