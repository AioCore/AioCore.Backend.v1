using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Package.Mongo.Models;

namespace Package.Mongo.Abstractions
{
    public interface IMongoRepository<TKey> where TKey : IEquatable<TKey>
    {
        string ConnectionString { get; }
        
        string DatabaseName { get; }
        
        #region UpdateClientSession

        bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        bool UpdateOne<TDocument, TKey>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        bool UpdateOne<TDocument, TKey>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<bool> UpdateOneAsync<TDocument, TKey>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<bool> UpdateOneAsync<TDocument, TKey>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

            #endregion

        #region Update

        Task<bool> UpdateOneAsync<TDocument, TKey>(TDocument modifiedDocument)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        bool UpdateOne<TDocument, TKey>(TDocument modifiedDocument)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<bool> UpdateOneAsync<TDocument, TKey>(TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        bool UpdateOne<TDocument, TKey>(TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        bool UpdateOne<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        bool UpdateOne<TDocument, TKey, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        bool UpdateOne<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<long> UpdateManyAsync<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<long> UpdateManyAsync<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<long> UpdateManyAsync<TDocument, TKey>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<long> UpdateManyAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        long UpdateMany<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        long UpdateMany<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        long UpdateMany<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        long UpdateMany<TDocument, TKey>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        #endregion

        #region Get

        Task<List<TDocument>> GetPaginatedAsync<TDocument>(Expression<Func<TDocument, bool>> filter, int skipNumber = 0, int takeNumber = 50, string partitionKey = null)
            where TDocument : IDocument;
        
        Task<List<TDocument>> GetPaginatedAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, int skipNumber = 0, int takeNumber = 50, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<TDocument> GetAndUpdateOne<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, FindOneAndUpdateOptions<TDocument, TDocument> options)
            where TDocument : IDocument;
        
        Task<TDocument> GetAndUpdateOne<TDocument, TKey>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, FindOneAndUpdateOptions<TDocument, TDocument> options)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        #endregion

        #region Readonly

        Task<TDocument> GetByIdAsync<TDocument, TKey>(TKey id, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        TDocument GetById<TDocument, TKey>(TKey id, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<TDocument> GetOneAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        TDocument GetOne<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        IFindFluent<TDocument, TDocument> GetCursor<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<bool> AnyAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        bool Any<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<List<TDocument>> GetAllAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        List<TDocument> GetAll<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<long> CountAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        long Count<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<TDocument> GetByMaxAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        TDocument GetByMax<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<TDocument> GetByMinAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        TDocument GetByMin<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<TValue> GetMaxValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        TValue GetMaxValue<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<TValue> GetMinValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        TValue GetMinValue<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<int> SumByAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        int SumBy<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<decimal> SumByAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        decimal SumBy<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<TProjection> ProjectOneAsync<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class;
        
        TProjection ProjectOne<TDocument, TProjection, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class;
        
        Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class;
        
        List<TProjection> ProjectMany<TDocument, TProjection, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class;
        
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new();
        
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new();
        
        Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> sortSelector,
            bool ascending = true,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
        
        Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            SortDefinition<TDocument> sortDefinition,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        #endregion

        #region Read

        Task<TDocument> GetByIdAsync<TDocument>(TKey id, string partitionKey = null) where TDocument : IDocument<TKey>;
        
        TDocument GetById<TDocument>(TKey id, string partitionKey = null) where TDocument : IDocument<TKey>;
        
        TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>;
        
        IFindFluent<TDocument, TDocument> GetCursor<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>;
        
        Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>;
        
        bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>;
        
        Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>;
        
        List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>;
        
        Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>;
        
        long Count<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey>;
        
        Task<TDocument> GetByMaxAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending, string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        TDocument GetByMax<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending, string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        Task<TDocument> GetByMinAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByAscending, string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        TDocument GetByMin<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByAscending, string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        Task<TValue> GetMaxValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        TValue GetMaxValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        Task<TValue> GetMinValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        TValue GetMinValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        Task<int> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        Task<decimal> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        int SumBy<TDocument>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        decimal SumBy<TDocument>(Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        Task<TProjection> ProjectOneAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TProjection : class;
        
        TProjection ProjectOne<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TProjection : class;
        
        Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TProjection : class;
        
        List<TProjection> ProjectMany<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TProjection : class;
        
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TProjection : class, new();
        
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TProjection : class, new();
        
        Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> sortSelector,
            bool ascending = true,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            SortDefinition<TDocument> sortDefinition,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null)
            where TDocument : IDocument<TKey>;

        #endregion

        #region Create

        Task AddOneAsync<TDocument>(TDocument document, CancellationToken cancellationToken = default) where TDocument : IDocument<TKey>;
        
        void AddOne<TDocument>(TDocument document) where TDocument : IDocument<TKey>;
        
        Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default) where TDocument : IDocument<TKey>;
        
        void AddMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument<TKey>;

        #endregion
    }
}