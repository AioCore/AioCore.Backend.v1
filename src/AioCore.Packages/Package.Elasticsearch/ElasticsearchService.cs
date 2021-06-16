using AioCore.Shared.Common;
using AioCore.Shared.Exceptions;
using Elasticsearch.Net;
using Nest;
using Package.Elasticsearch.Common;
using Package.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Operator = AioCore.Shared.Common.Operator;

namespace Package.Elasticsearch
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticsearchService(IElasticClientFactory elasticClientFactory)
        {
            _elasticClient = elasticClientFactory.CreateElasticClient();
        }

        public async Task IndexAsync<T>(T document) where T : class
        {
            var response = await _elasticClient.IndexAsync(document,
                descriptor => descriptor);
            if (!response.IsValid)
                throw new ApplicationException(response.DebugInformation);
            await _elasticClient.Indices.RefreshAsync(Indices.All);
        }

        public async Task IndexAsync<T>(T document, string id) where T : class
        {
            var response = await _elasticClient.IndexAsync(document,
                descriptor => descriptor.Id(id));
            if (!response.IsValid)
                throw new ApplicationException(response.DebugInformation);
            await _elasticClient.Indices.RefreshAsync(Indices.All);
        }

        public async Task IndexManyAsync<T>(List<T> documents) where T : class
        {
            if (documents.Count == 0) return;
            var response = await _elasticClient.IndexManyAsync(documents);
            if (!response.IsValid)
                throw new ApplicationException(response.DebugInformation);
            await _elasticClient.Indices.RefreshAsync(Indices.All);
        }

        public async Task UpdateAsync<T>(T document) where T : class
        {
            var dictDocument = document.ToDictionary();
            await _elasticClient.UpdateAsync(DocumentPath<T>.Id(dictDocument["Id"].ToString()),
                descriptor => descriptor.DocAsUpsert().Doc(document));
            await _elasticClient.Indices.RefreshAsync(Indices.All);
        }

        public async Task DeleteAsync<T>(Guid id) where T : class
        {
            await _elasticClient.DeleteAsync(DocumentPath<T>.Id(id));
            await _elasticClient.Indices.RefreshAsync(Indices.All);
        }

        public async Task<Pagination<T>> SearchAsync<T>(int page, int pageSize, string keyword, List<QueryAdvanced> filter = null) where T : class
        {
            var searchRequest = new SearchRequest<T>
            {
                Query = BuildBoolQuery<T>(keyword, filter),
                From = (page - 1) * pageSize,
                Size = pageSize
            };
#if DEBUG
            var json = _elasticClient.RequestResponseSerializer.SerializeToString(searchRequest);
            Debug.WriteLine(json);
#endif
            var response = await _elasticClient.SearchAsync<T>(searchRequest);
            return new Pagination<T>(page, pageSize, response.Documents.ToList(), response.Total);
        }

        public async Task<List<T>> SearchAsync<T>(string keyword, List<QueryAdvanced> filter) where T : class
        {
            var searchRequest = new SearchRequest<T>
            {
                Query = BuildBoolQuery<T>(keyword, filter)
            };
#if DEBUG
            var json = _elasticClient.RequestResponseSerializer.SerializeToString(searchRequest);
            Debug.WriteLine(json);
#endif
            var response = await _elasticClient.SearchAsync<T>(searchRequest);
            return response.Documents.ToList();
        }

        public async Task<T> GetAsync<T>(string id) where T : class
        {
            var response = await _elasticClient.GetAsync<T>(id);
            if (response.IsValid)
            {
                return response?.Source;
            }
            throw new AioCoreException(response.OriginalException?.Message, response.OriginalException);
        }

        public async Task<Pagination<object>> SearchDynamicAsync(Guid typeId, int page, int pageSize, List<QueryAdvanced> filter = null)
        {
            var searchRequest = new SearchRequest
            {
                Query = BuildDynamicBoolQuery(typeId, filter),
                From = (page - 1) * pageSize,
                Size = pageSize
            };
#if DEBUG
            var json = _elasticClient.RequestResponseSerializer.SerializeToString(searchRequest);
            Debug.WriteLine(json);
#endif
            var response = await _elasticClient.SearchAsync<dynamic>(searchRequest);
            return new Pagination<dynamic>(page, pageSize, response.Documents.ToList(), response.Total);
        }

        #region private

        private static QueryBaseContainer BuildQuery(List<QueryAdvanced> filters)
        {
            if (filters?.Any() != true) return null;

            var must = new List<QueryContainer>();
            var should = new List<QueryContainer>();
            var mustNot = new List<QueryContainer>();

            foreach (var filter in filters)
            {
                var queryContainer = CreateQueryBase(filter);
                switch (filter.Operator)
                {
                    case Operator.AND: must.Add(queryContainer); break;
                    case Operator.OR: should.Add(queryContainer); break;
                    case Operator.NOT: mustNot.Add(queryContainer); break;
                }
            }

            return new QueryBaseContainer
            {
                Must = must,
                Should = should,
                MustNot = mustNot
            };
        }

        private static BoolQuery BuildBoolQuery<T>(string keyword, List<QueryAdvanced> filter)
        {
            var queryBase = BuildQuery(filter) ?? new QueryBaseContainer();
            queryBase.Must.AddRange(new List<QueryContainer>
            {
                new MatchQuery {Field = "indexType", Query = typeof(T).Name},
                new SimpleQueryStringQuery {Query = keyword}
            });
            return new BoolQuery
            {
                Must = queryBase.Must,
                Should = queryBase.Should,
                MustNot = queryBase.MustNot
            };
        }

        private static BoolQuery BuildDynamicBoolQuery(Guid typeId, List<QueryAdvanced> filter)
        {
            var queryBase = BuildQuery(filter) ?? new QueryBaseContainer();
            queryBase.Must.AddRange(new List<QueryContainer>
            {
                new MatchQuery {Field = "EntityTypeId.keyword", Query = typeId.ToString()},
            });
            return new BoolQuery
            {
                Must = queryBase.Must,
                Should = queryBase.Should,
                MustNot = queryBase.MustNot
            };
        }

        private static QueryBase CreateQueryBase(QueryAdvanced query)
        {
            switch (query.Function)
            {
                case Function.Equal:
                    switch (query.ValueType)
                    {
                        case DataType.Text:
                        case DataType.Number:
                            return new MatchQuery { Field = query.Field, Query = query.Value.ToString() };
                    }
                    break;

                case Function.Between:
                    var rangeItems = query.Value.ToString().Split("|");
                    switch (query.ValueType)
                    {
                        case DataType.Number:
                            return new NumericRangeQuery
                            {
                                Field = query.Field,
                                GreaterThanOrEqualTo = int.Parse(rangeItems[0]),
                                LessThanOrEqualTo = int.Parse(rangeItems[1])
                            };

                        case DataType.DateTime:
                            return new DateRangeQuery
                            {
                                Field = query.Field,
                                GreaterThanOrEqualTo = DateMath.FromString(rangeItems[0]),
                                LessThanOrEqualTo = DateMath.FromString(rangeItems[1])
                            };
                    }
                    break;

                case Function.NotIn:
                case Function.NotEqual:
                    switch (query.ValueType)
                    {
                        case DataType.Guid:
                            return new BoolQuery { MustNot = new List<QueryContainer>(Terms<Guid>(query.Value, query.Field)) };
                        case DataType.Number:
                            return new BoolQuery { MustNot = new List<QueryContainer>(Terms<int>(query.Value, query.Field)) };
                        case DataType.Float:
                            return new BoolQuery { MustNot = new List<QueryContainer>(Terms<float>(query.Value, query.Field)) };
                    }
                    break;

                case Function.In:
                    switch (query.ValueType)
                    {
                        case DataType.Guid:
                            return new BoolQuery { Must = new List<QueryContainer>(Terms<Guid>(query.Value, query.Field)) };
                        case DataType.Number:
                            return new BoolQuery { Must = new List<QueryContainer>(Terms<int>(query.Value, query.Field)) };
                        case DataType.Float:
                            return new BoolQuery { Must = new List<QueryContainer>(Terms<float>(query.Value, query.Field)) };
                    }
                    break;
            }
            throw new ArgumentOutOfRangeException();
        }

        private static IEnumerable<QueryContainer> Terms<TValueType>(object value, string field)
        {
            var terms = new List<QueryContainer>();
            value.ToJson().ParseTo<List<TValueType>>().ForEach(y => terms.Add(new TermQuery { Field = field, Value = y }));
            return terms;
        }

        #endregion private
    }
}