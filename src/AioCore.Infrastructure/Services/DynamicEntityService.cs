using AioCore.Application.Services;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.DynamicAggregatesModel;
using Microsoft.EntityFrameworkCore;
using Package.Elasticsearch;
using Package.Redis;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Services
{
    public class DynamicEntityService : IDynamicEntityService
    {
        private static readonly string _typeName = nameof(DynamicEntityService);
        private readonly ICacheManager _cacheManager;
        private readonly IAioDynamicUnitOfWork _dynamicUnitOfWork;
        private readonly IElasticsearchService _elasticsearchService;

        public DynamicEntityService(
              ICacheManager cacheManager
            , IAioDynamicUnitOfWork dynamicUnitOfWork
            , IElasticsearchService elasticsearchService
        )
        {
            _cacheManager = cacheManager;
            _dynamicUnitOfWork = dynamicUnitOfWork;
            _elasticsearchService = elasticsearchService;
        }

        public async Task<Dictionary<string, object>> GetDynamicEntityAsync(Guid id)
        {
            return await _cacheManager.GetOrSetAsync($"{_typeName}_GetDynamicEntityAsync_{id}",
                async () =>
                {
                    var entity = await _dynamicUnitOfWork.DynamicEntities
                        .Include(t => t.DynamicDateValues)
                        .Include(t => t.DynamicFloatValues)
                        .Include(t => t.DynamicGuidValues)
                        .Include(t => t.DynamicIntegerValues)
                        .Include(t => t.DynamicStringValues)
                        .FirstOrDefaultAsync(t => t.Id == id);

                    if (entity == null) return new Dictionary<string, object>();

                    var attributes = (await _dynamicUnitOfWork.DynamicAttributes
                        .Where(t => t.EntityTypeId == entity.EntityTypeId)
                        .ToListAsync())
                        .ToDictionary(t => t.Id, t => t.Name);

                    var dict = new Dictionary<string, object>()
                    {
                        { nameof(entity.Id), entity.Id },
                        { nameof(entity.Name), entity.Name }
                    };

                    void Add<T, TType>(IEnumerable<T> values) where T : DynamicValue<TType>
                    {
                        if (values?.Any() != true) return;
                        foreach (var item in values)
                        {
                            var attrName = attributes.GetValueOrDefault(item.AttributeId);
                            if (!string.IsNullOrEmpty(attrName) && !dict.ContainsKey(attrName))
                            {
                                dict.Add(attributes[item.AttributeId], item.Value);
                            }
                        }
                    }

                    Add<DynamicDateValue, DateTimeOffset>(entity.DynamicDateValues);
                    Add<DynamicFloatValue, float>(entity.DynamicFloatValues);
                    Add<DynamicGuidValue, Guid>(entity.DynamicGuidValues);
                    Add<DynamicIntegerValue, int>(entity.DynamicIntegerValues);
                    Add<DynamicStringValue, string>(entity.DynamicStringValues);

                    return dict;
                });
        }

        public async Task IndexAsync(DynamicEntity entity)
        {
            //index to elasticsearch
            IDictionary<string, object> indexObject = new ExpandoObject();
            void Add<T, TType>(ICollection<T> entities) where T : DynamicValue<TType>
            {
                if (entities?.Any() != true) return;
                foreach (var item in entities)
                {
                    indexObject.Add(item.Attribute.Name, item.Value);
                }
            }

            indexObject.Add("Id", entity.Id);
            indexObject.Add("Name", entity.Name);
            indexObject.Add("EntityTypeId", entity.EntityTypeId);
            indexObject.Add("TenantId", entity.TenantId);
            Add<DynamicDateValue, DateTimeOffset>(entity.DynamicDateValues);
            Add<DynamicIntegerValue, int>(entity.DynamicIntegerValues);
            Add<DynamicFloatValue, float>(entity.DynamicFloatValues);
            Add<DynamicGuidValue, Guid>(entity.DynamicGuidValues);
            Add<DynamicStringValue, string>(entity.DynamicStringValues);

            await _elasticsearchService.IndexAsync<dynamic>(indexObject, entity.Id.ToString());
        }
    }
}
