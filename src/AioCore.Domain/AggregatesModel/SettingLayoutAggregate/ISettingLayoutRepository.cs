using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.SettingLayoutAggregate
{
    public interface ISettingLayoutRepository : IRepository<SettingLayout>
    {
        SettingLayout Add(SettingLayout layout);

        void Update(SettingLayout layout);

        void Delete(Guid id);
    }
}