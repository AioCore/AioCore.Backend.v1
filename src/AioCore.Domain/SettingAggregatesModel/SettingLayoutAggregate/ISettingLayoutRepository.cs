using System;
using System.Threading.Tasks;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.SettingAggregatesModel.SettingLayoutAggregate
{
    public interface ISettingLayoutRepository : IRepository<SettingLayout>
    {
        Task<SettingLayout> GetAsync(Guid id);

        SettingLayout Add(SettingLayout layout);

        void Update(SettingLayout layout);

        void Delete(Guid id);
    }
}