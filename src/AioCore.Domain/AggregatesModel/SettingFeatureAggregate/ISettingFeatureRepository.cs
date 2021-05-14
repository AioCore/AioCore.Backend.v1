using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.SettingFeatureAggregate
{
    public interface ISettingFeatureRepository : IRepository<SettingFeature>
    {
        SettingFeature Add(SettingFeature feature);

        void Update(SettingFeature feature);

        void Delete(Guid id);
    }
}