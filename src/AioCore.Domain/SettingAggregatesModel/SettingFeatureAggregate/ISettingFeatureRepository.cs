using System;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.SettingAggregatesModel.SettingFeatureAggregate
{
    public interface ISettingFeatureRepository : IRepository<SettingFeature>
    {
        SettingFeature Add(SettingFeature feature);

        void Update(SettingFeature feature);

        void Delete(Guid id);
    }
}