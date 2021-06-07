using AioCore.Application.Services;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.DynamicAggregatesModel;
using Microsoft.EntityFrameworkCore;
using Package.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Services
{
    public class DynamicEntityService : IDynamicEntityService
    {
        private static readonly string _typeName = nameof(DynamicEntityService);
        private readonly ICacheManager _cacheManager;
        private readonly IAioDynamicUnitOfWork _dynamicUnitOfWork;

        public DynamicEntityService(
            ICacheManager cacheManager
            , IAioDynamicUnitOfWork dynamicUnitOfWork
        )
        {
            _cacheManager = cacheManager;
            _dynamicUnitOfWork = dynamicUnitOfWork;
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

                    void Add<T>(IEnumerable<DynamicValue<T>> values)
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

                    Add(entity.DynamicDateValues.OfType<DynamicValue<DateTimeOffset>>());
                    Add(entity.DynamicFloatValues.OfType<DynamicValue<float>>());
                    Add(entity.DynamicGuidValues.OfType<DynamicValue<Guid>>());
                    Add(entity.DynamicIntegerValues.OfType<DynamicValue<int>>());
                    Add(entity.DynamicStringValues.OfType<DynamicValue<string>>());

                    return dict;
                });
        }
    }
}
