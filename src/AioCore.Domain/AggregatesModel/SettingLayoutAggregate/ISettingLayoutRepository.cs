using AioCore.Shared.Seedwork;
using System;
using System.Threading.Tasks;

namespace AioCore.Domain.AggregatesModel.SettingLayoutAggregate
{
    public interface ISettingLayoutRepository : IRepository<SettingLayout>
    {
        Task<SettingLayout> GetAsync(Guid id);

        SettingLayout Add(SettingLayout layout);

        void Update(SettingLayout layout);

        void Delete(Guid id);
    }
}