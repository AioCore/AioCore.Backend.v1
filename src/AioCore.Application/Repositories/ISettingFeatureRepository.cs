using AioCore.Domain.SettingAggregatesModel.SettingFeatureAggregate;
using System;

namespace AioCore.Application.Repositories
{
    public interface ISettingFeatureRepository : IRepository<SettingFeature>
    {
        void Delete(Guid id);
    }
}