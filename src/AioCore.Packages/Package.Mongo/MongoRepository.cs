using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Package.Mongo.Abstractions;
using Package.Mongo.DataAccess;
using Package.Mongo.Models;

namespace Package.Mongo
{
    public class MongoRepository<TKey> : IMongoRepository<TKey> where TKey : IEquatable<TKey>
    {
        public string ConnectionString { get; }
        public string DatabaseName { get; }
        
        private MongoDbCreator _mongoDbCreator;

        public bool UpdateOne<TDocument, TKey1, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field,
            TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken)) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne<TDocument, TKey1, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field,
            TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken)) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne<TDocument, TKey1, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field,
            TField value, CancellationToken cancellationToken = default(CancellationToken)) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne<TDocument, TKey1>(IClientSessionHandle session, TDocument modifiedDocument,
            CancellationToken cancellationToken = default(CancellationToken)) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne<TDocument, TKey1>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update,
            CancellationToken cancellationToken = default(CancellationToken)) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync<TDocument, TKey1, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field,
            TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken)) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync<TDocument, TKey1, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field,
            TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken)) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync<TDocument, TKey1, TField>(IClientSessionHandle session, TDocument documentToModify,
            Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default(CancellationToken)) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync<TDocument, TKey1>(IClientSessionHandle session, TDocument modifiedDocument,
            CancellationToken cancellationToken = default(CancellationToken)) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync<TDocument, TKey1>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update,
            CancellationToken cancellationToken = default(CancellationToken)) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync<TDocument, TKey1>(TDocument modifiedDocument) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne<TDocument, TKey1>(TDocument modifiedDocument) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync<TDocument, TKey1>(TDocument documentToModify, UpdateDefinition<TDocument> update) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne<TDocument, TKey1>(TDocument documentToModify, UpdateDefinition<TDocument> update) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne<TDocument, TKey1, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync<TDocument, TKey1, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync<TDocument, TKey1, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne<TDocument, TKey1, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync<TDocument, TKey1, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne<TDocument, TKey1, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateManyAsync<TDocument, TKey1, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateManyAsync<TDocument, TKey1, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateManyAsync<TDocument, TKey1>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateManyAsync<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public long UpdateMany<TDocument, TKey1, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public long UpdateMany<TDocument, TKey1, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public long UpdateMany<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public long UpdateMany<TDocument, TKey1>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<List<TDocument>> GetPaginatedAsync<TDocument>(Expression<Func<TDocument, bool>> filter, int skipNumber = 0, int takeNumber = 50,
            string partitionKey = null) where TDocument : IDocument
        {
            throw new NotImplementedException();
        }

        public Task<List<TDocument>> GetPaginatedAsync<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, int skipNumber = 0, int takeNumber = 50,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetAndUpdateOne<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, FindOneAndUpdateOptions<TDocument, TDocument> options) where TDocument : IDocument
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetAndUpdateOne<TDocument, TKey1>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update,
            FindOneAndUpdateOptions<TDocument, TDocument> options) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetByIdAsync<TDocument, TKey1>(TKey1 id, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public TDocument GetById<TDocument, TKey1>(TKey1 id, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetOneAsync<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public TDocument GetOne<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public IFindFluent<TDocument, TDocument> GetCursor<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public bool Any<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<List<TDocument>> GetAllAsync<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public List<TDocument> GetAll<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public long Count<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetByMaxAsync<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public TDocument GetByMax<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetByMinAsync<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public TDocument GetByMin<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<TValue> GetMaxValueAsync<TDocument, TKey1, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public TValue GetMaxValue<TDocument, TKey1, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<TValue> GetMinValueAsync<TDocument, TKey1, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public TValue GetMinValue<TDocument, TKey1, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<int> SumByAsync<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public int SumBy<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<decimal> SumByAsync<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public decimal SumBy<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> ProjectOneAsync<TDocument, TProjection, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection,
            string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TProjection : class where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public TProjection ProjectOne<TDocument, TProjection, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TProjection : class where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection,
            string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TProjection : class where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public List<TProjection> ProjectMany<TDocument, TProjection, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null) where TDocument : IDocument<TKey1> where TProjection : class where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey1>(Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey = null) where TDocument : IDocument<TKey1> where TProjection : class, new() where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection, string partitionKey = null) where TDocument : IDocument<TKey1> where TProjection : class, new() where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> sortSelector, bool @ascending = true,
            int skipNumber = 0, int takeNumber = 50, string partitionKey = null) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey1>(Expression<Func<TDocument, bool>> filter, SortDefinition<TDocument> sortDefinition, int skipNumber = 0,
            int takeNumber = 50, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IDocument<TKey1> where TKey1 : IEquatable<TKey1>
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetByIdAsync<TDocument>(TKey id, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public TDocument GetById<TDocument>(TKey id, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public IFindFluent<TDocument, TDocument> GetCursor<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public long Count<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetByMaxAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public TDocument GetByMax<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> GetByMinAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByAscending, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public TDocument GetByMin<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByAscending, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<TValue> GetMaxValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public TValue GetMaxValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<TValue> GetMinValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public TValue GetMinValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<int> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<decimal> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public int SumBy<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public decimal SumBy<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> ProjectOneAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null) where TDocument : IDocument<TKey> where TProjection : class
        {
            throw new NotImplementedException();
        }

        public TProjection ProjectOne<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null) where TDocument : IDocument<TKey> where TProjection : class
        {
            throw new NotImplementedException();
        }

        public Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null) where TDocument : IDocument<TKey> where TProjection : class
        {
            throw new NotImplementedException();
        }

        public List<TProjection> ProjectMany<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null) where TDocument : IDocument<TKey> where TProjection : class
        {
            throw new NotImplementedException();
        }

        public List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey = null) where TDocument : IDocument<TKey> where TProjection : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection, string partitionKey = null) where TDocument : IDocument<TKey> where TProjection : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> sortSelector, bool @ascending = true,
            int skipNumber = 0, int takeNumber = 50, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>(Expression<Func<TDocument, bool>> filter, SortDefinition<TDocument> sortDefinition, int skipNumber = 0,
            int takeNumber = 50, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task AddOneAsync<TDocument>(TDocument document, CancellationToken cancellationToken = default) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public void AddOne<TDocument>(TDocument document) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }

        public void AddMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument<TKey>
        {
            throw new NotImplementedException();
        }
    }
}