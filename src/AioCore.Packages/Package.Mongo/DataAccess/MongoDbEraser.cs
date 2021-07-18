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
    public class MongoDbEraser : DataAccessBase
    {
        public MongoDbEraser(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }
        
        public virtual long DeleteOne<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", document.Id);
            return HandlePartitioned<TDocument, TKey>(document).DeleteOne(filter).DeletedCount;
        }
        
        public virtual async Task<long> DeleteOneAsync<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", document.Id);
            return (await HandlePartitioned<TDocument, TKey>(document).DeleteOneAsync(filter)).DeletedCount;
        }
        
        public virtual long DeleteOne<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).DeleteOne(filter).DeletedCount;
        }
        
        public virtual async Task<long> DeleteOneAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return (await HandlePartitioned<TDocument, TKey>(partitionKey).DeleteOneAsync(filter)).DeletedCount;
        }
        
        public virtual async Task<long> DeleteManyAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return (await HandlePartitioned<TDocument, TKey>(partitionKey).DeleteManyAsync(filter)).DeletedCount;
        }
        
        public virtual async Task<long> DeleteManyAsync<TDocument, TKey>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var enumerable = documents as TDocument[] ?? documents.ToArray();
            if (!enumerable.Any())
            {
                return 0;
            }
            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (enumerable.Any(e => e is IPartitionedDocument))
            {
                long deleteCount = 0;
                foreach (var group in enumerable.GroupBy(e => ((IPartitionedDocument)e).PartitionKey))
                {
                    var groupIdsDelete = group.Select(e => e.Id).ToArray();
                    deleteCount += (await HandlePartitioned<TDocument, TKey>(group.FirstOrDefault()).DeleteManyAsync(x => groupIdsDelete.Contains(x.Id))).DeletedCount;
                }
                return deleteCount;
            }
            else
            {
                var idsDelete = enumerable.Select(e => e.Id).ToArray();
                return (await HandlePartitioned<TDocument, TKey>(enumerable.FirstOrDefault()).DeleteManyAsync(x => idsDelete.Contains(x.Id))).DeletedCount;
            }
        }
        
        public virtual long DeleteMany<TDocument, TKey>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var enumerable = documents as TDocument[] ?? documents.ToArray();
            if (!enumerable.Any())
            {
                return 0;
            }

            if (enumerable.Any(e => e is IPartitionedDocument))
            {
                return (from @group in enumerable.GroupBy(e => ((IPartitionedDocument) e).PartitionKey) let groupIdsDelete = @group.Select(e => e.Id).ToArray() select (HandlePartitioned<TDocument, TKey>(@group.FirstOrDefault()).DeleteMany(x => groupIdsDelete.Contains(x.Id))).DeletedCount).Sum();
            }
            else
            {
                var idsDelete = enumerable.Select(e => e.Id).ToArray();
                return (HandlePartitioned<TDocument, TKey>(enumerable.FirstOrDefault()).DeleteMany(x => idsDelete.Contains(x.Id))).DeletedCount;
            }
        }
        
        public virtual long DeleteMany<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).DeleteMany(filter).DeletedCount;
        }
    }
}