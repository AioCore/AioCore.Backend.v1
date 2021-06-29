using AioCore.Domain.DynamicEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AioCore.Application.Services
{
    public interface IDynamicEntityService
    {
        Task<Dictionary<string, object>> GetDynamicEntityAsync(Guid id);
        Task IndexAsync(DynamicEntity entity);
    }
}
