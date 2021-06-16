using AioCore.Shared.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Package.Elasticsearch
{
    public interface IElasticsearchService
    {
        Task IndexAsync<T>(T document) where T : class;

        Task IndexAsync<T>(T document, string id) where T : class;

        Task IndexManyAsync<T>(List<T> documents) where T : class;

        Task UpdateAsync<T>(T document) where T : class;

        Task DeleteAsync<T>(Guid id) where T : class;

        Task<Pagination<T>> SearchAsync<T>(int page, int pageSize, string keyword, List<QueryAdvanced> filter = null) where T : class;

        Task<List<T>> SearchAsync<T>(string keyword, List<QueryAdvanced> filter) where T : class;

        Task<T> GetAsync<T>(string id) where T : class;
        
        Task<Pagination<object>> SearchDynamicAsync(Guid typeId, int page, int pageSize, List<QueryAdvanced> filter = null);
    }
}