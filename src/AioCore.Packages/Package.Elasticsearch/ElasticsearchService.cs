using Elasticsearch.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nest;
using Package.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Package.Elasticsearch
{
    public interface IElasticsearchService
    {
        Task IndexAsync<T>(T document) where T : class;

        Task IndexManyAsync<T>(List<T> documents) where T : class;

        Task UpdateAsync<T>(T document) where T : class;

        Task DeleteAsync<T>(Guid id) where T : class;

        Task<Pagination<T>> SearchAsync<T>(int page, int pageSize, string keyword, List<QueryAdvanced> filter = null) where T : class;

        Task<List<T>> SearchAsync<T>(string keyword, List<QueryAdvanced> filter) where T : class;
    }

    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticsearchService(IServiceProvider serviceProvider)
        {
            _elasticClient = serviceProvider.GetRequiredService<IElasticClient>();
            serviceProvider.GetRequiredService<ILogger<ElasticsearchService>>();
        }

        public async Task IndexAsync<T>(T document) where T : class
        {
            var response = await _elasticClient.IndexAsync(document,
                descriptor => descriptor);
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
            var queryContainers = QueryContainers<T>(keyword, filter);

            var searchRequest = new SearchRequest<T>
            {
                Query = new BoolQuery { Must = queryContainers },
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
            var queryContainers = QueryContainers<T>(keyword, filter);
            var searchRequest = new SearchRequest<T>
            {
                Query = new BoolQuery { Must = queryContainers },
            };
#if DEBUG
            var json = _elasticClient.RequestResponseSerializer.SerializeToString(searchRequest);
            Debug.WriteLine(json);
#endif
            var response = await _elasticClient.SearchAsync<T>(searchRequest);
            return response.Documents.ToList();
        }

        private static IEnumerable<QueryContainer> QueryContainers<T>(string keyword, List<QueryAdvanced> filter)
        {
            var queryContainers = new List<QueryContainer>
            {
                new MatchQuery {Field = "indexType", Query = typeof(T).Name},
                new SimpleQueryStringQuery {Query = keyword}
            };

            filter?.ForEach(x =>
            {
                switch (x.Function)
                {
                    case Function.Equal:
                        switch (x.ValueType)
                        {
                            case DataType.Text:
                            case DataType.Number:
                                queryContainers.Add(new MatchQuery { Field = x.Field, Query = x.Value.ToString() });
                                break;
                        }
                        break;

                    case Function.Between:
                        var rangeItems = x.Value.ToString().Split("|");
                        switch (x.ValueType)
                        {
                            case DataType.Number:
                                queryContainers.Add(new NumericRangeQuery
                                {
                                    Field = x.Field,
                                    GreaterThanOrEqualTo = int.Parse(rangeItems[0]),
                                    LessThanOrEqualTo = int.Parse(rangeItems[1])
                                });
                                break;

                            case DataType.DateTime:
                                queryContainers.Add(new DateRangeQuery
                                {
                                    Field = x.Field,
                                    GreaterThanOrEqualTo = DateMath.FromString(rangeItems[0]),
                                    LessThanOrEqualTo = DateMath.FromString(rangeItems[1])
                                });
                                break;
                        }
                        break;

                    case Function.NotIn:
                    case Function.NotEqual:
                        switch (x.ValueType)
                        {
                            case DataType.Guid:
                                queryContainers.Add(new BoolQuery { MustNot = new List<QueryContainer>(Terms<Guid>(x.Value, x.Field)) });
                                break;

                            case DataType.Number:
                                queryContainers.Add(new BoolQuery { MustNot = new List<QueryContainer>(Terms<int>(x.Value, x.Field)) });
                                break;
                        }
                        break;

                    case Function.In:
                        switch (x.ValueType)
                        {
                            case DataType.Guid:
                                queryContainers.Add(new BoolQuery { Must = new List<QueryContainer>(Terms<Guid>(x.Value, x.Field)) });
                                break;

                            case DataType.Number:
                                queryContainers.Add(new BoolQuery { Must = new List<QueryContainer>(Terms<int>(x.Value, x.Field)) });
                                break;
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
            return queryContainers;

            static IEnumerable<QueryContainer> Terms<TValueType>(object value, string field)
            {
                var terms = new List<QueryContainer>();
                value.ToJson().ParseTo<List<TValueType>>().ForEach(y => terms.Add(new TermQuery { Field = field, Value = y }));
                return terms;
            }
        }
    }
}