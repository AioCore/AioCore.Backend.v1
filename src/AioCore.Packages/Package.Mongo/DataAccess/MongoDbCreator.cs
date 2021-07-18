using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Package.Mongo.Abstractions;
using Package.Mongo.Models;
using Package.Mongo.Utils;

namespace Package.Mongo.DataAccess
{
    public class MongoDbCreator : DataAccessBase
    {
        public MongoDbCreator(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }
        
        public virtual async Task AddOneAsync<TDocument, TKey>(TDocument document, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            FormatDocument<TDocument, TKey>(document);
            await HandlePartitioned<TDocument, TKey>(document).InsertOneAsync(document, null, cancellationToken);
        }
        
        public virtual void AddOne<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            FormatDocument<TDocument, TKey>(document);
            HandlePartitioned<TDocument, TKey>(document).InsertOne(document);
        }
        
        public virtual async Task AddManyAsync<TDocument, TKey>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var enumerable = documents as TDocument[] ?? documents.ToArray();
            if (!enumerable.Any())
            {
                return;
            }
            foreach (var document in enumerable)
            {
                FormatDocument<TDocument, TKey>(document);
            }

            if (enumerable.Any(e => e is IPartitionedDocument))
            {
                foreach (var group in enumerable.GroupBy(e => ((IPartitionedDocument)e).PartitionKey))
                {
                    await HandlePartitioned<TDocument, TKey>(group.FirstOrDefault()).InsertManyAsync(group.ToList(), null, cancellationToken);
                }
            }
            else
            {
                await GetCollection<TDocument, TKey>().InsertManyAsync(enumerable.ToList(), null, cancellationToken);
            }
        }
        
        public virtual void AddMany<TDocument, TKey>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var enumerable = documents as TDocument[] ?? documents.ToArray();
            if (!enumerable.Any())
            {
                return;
            }
            foreach (var document in enumerable)
            {
                FormatDocument<TDocument, TKey>(document);
            }

            if (enumerable.Any(e => e is IPartitionedDocument))
            {
                foreach (var group in enumerable.GroupBy(e => ((IPartitionedDocument)e).PartitionKey))
                {
                    HandlePartitioned<TDocument, TKey>(group.FirstOrDefault()).InsertMany(group.ToList());
                }
            }
            else
            {
                GetCollection<TDocument, TKey>().InsertMany(enumerable.ToList());
            }
        }
        
        protected static void FormatDocument<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }
            var defaultTKey = default(TKey);
            if (document.Id == null
                || (defaultTKey != null
                    && defaultTKey.Equals(document.Id)))
            {
                document.Id = IdGenerator.GetId<TKey>();
            }
        }
    }
}