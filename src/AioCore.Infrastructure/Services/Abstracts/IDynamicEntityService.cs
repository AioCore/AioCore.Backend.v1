using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AioCore.Domain.DynamicEntities;

namespace AioCore.Infrastructure.Services.Abstracts
{
    public interface IDynamicEntityService
    {
        Task<Dictionary<string, object>> GetDynamicEntityAsync(Guid id);

        Task IndexAsync(DynamicEntity entity);
    }
}