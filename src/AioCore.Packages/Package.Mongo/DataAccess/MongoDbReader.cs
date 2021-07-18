using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Package.Mongo.Abstractions;
using Package.Mongo.Models;

namespace Package.Mongo.DataAccess
{
    public class MongoDbReader : DataAccessBase
    {
        public MongoDbReader(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }
        
        public virtual async Task<TDocument> GetByIdAsync<TDocument, TKey>(TKey id, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", id);
            return await HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).FirstOrDefaultAsync(cancellationToken);
        }
        
        public virtual TDocument GetById<TDocument, TKey>(TKey id, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", id);
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).FirstOrDefault();
        }
        
        public virtual async Task<TDocument> GetOneAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).FirstOrDefaultAsync(cancellationToken);
        }
        
        public virtual TDocument GetOne<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).FirstOrDefault();
        }
        
        public virtual IFindFluent<TDocument, TDocument> GetCursor<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter);
        }
        
        public virtual async Task<bool> AnyAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var count = await HandlePartitioned<TDocument, TKey>(partitionKey).CountDocumentsAsync(filter, cancellationToken: cancellationToken);
            return (count > 0);
        }
        
        public virtual bool Any<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var count = HandlePartitioned<TDocument, TKey>(partitionKey).CountDocuments(filter);
            return (count > 0);
        }
        
        public virtual async Task<List<TDocument>> GetAllAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).ToListAsync(cancellationToken);
        }
        
        public virtual List<TDocument> GetAll<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).ToList();
        }
        
        public virtual async Task<long> CountAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey).CountDocumentsAsync(filter, cancellationToken: cancellationToken);
        }
        
        public virtual long Count<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).CountDocuments();
        }
        
        public virtual async Task<TDocument> GetByMaxAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetCollection<TDocument, TKey>(partitionKey).Find(Builders<TDocument>.Filter.Where(filter))
                .SortByDescending(maxValueSelector)
                .Limit(1)
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        public virtual TDocument GetByMax<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetCollection<TDocument, TKey>(partitionKey).Find(Builders<TDocument>.Filter.Where(filter))
                .SortByDescending(maxValueSelector)
                .Limit(1)
                .FirstOrDefault();
        }
        
        public virtual async Task<TDocument> GetByMinAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetCollection<TDocument, TKey>(partitionKey).Find(Builders<TDocument>.Filter.Where(filter))
                .SortBy(minValueSelector)
                .Limit(1)
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        public virtual TDocument GetByMin<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetCollection<TDocument, TKey>(partitionKey).Find(Builders<TDocument>.Filter.Where(filter))
                .SortBy(minValueSelector)
                .Limit(1)
                .FirstOrDefault();
        }
        
        public virtual async Task<TValue> GetMaxValueAsync<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetMaxMongoQuery<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey)
                .Project(maxValueSelector)
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        public virtual TValue GetMaxValue<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetMaxMongoQuery<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey)
                .Project(maxValueSelector)
                .FirstOrDefault();
        }
        
        public virtual async Task<TValue> GetMinValueAsync<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetMinMongoQuery<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey).Project(minValueSelector).FirstOrDefaultAsync(cancellationToken);
        }
        
        public virtual TValue GetMinValue<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetMinMongoQuery<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey).Project(minValueSelector).FirstOrDefault();
        }
        
        public virtual async Task<int> SumByAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetQuery<TDocument, TKey>(filter, partitionKey).SumAsync(selector, cancellationToken);
        }
        
        public virtual int SumBy<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetQuery<TDocument, TKey>(filter, partitionKey).Sum(selector);
        }
        
        public virtual async Task<decimal> SumByAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetQuery<TDocument, TKey>(filter, partitionKey).SumAsync(selector, cancellationToken);
        }
        
        public virtual decimal SumBy<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetQuery<TDocument, TKey>(filter, partitionKey).Sum(selector);
        }
    }

    public partial class MongoDbReader
    {
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Aggregate()
                .Group(groupingCriteria, groupProjection)
                .ToList();
        }
        
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> selector,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> projection,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            return collection.Aggregate()
                .Match(Builders<TDocument>.Filter.Where(filter))
                .Group(selector, projection)
                .ToList();
        }
        
        public virtual async Task<List<TProjection>> GroupByAsync<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> selector,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> projection,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            return await collection.Aggregate()
                .Match(Builders<TDocument>.Filter.Where(filter))
                .Group(selector, projection)
                .ToListAsync(cancellationToken);
        }
        
        public virtual async Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> sortSelector,
            bool ascending = true,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var sorting = ascending
                ? Builders<TDocument>.Sort.Ascending(sortSelector)
                : Builders<TDocument>.Sort.Descending(sortSelector);

            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .Sort(sorting)
                .Skip(skipNumber)
                .Limit(takeNumber)
                .ToListAsync(cancellationToken);
        }
        
        public virtual async Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            SortDefinition<TDocument> sortDefinition,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .Sort(sortDefinition)
                .Skip(skipNumber)
                .Limit(takeNumber)
                .ToListAsync(cancellationToken);
        }
    }
}