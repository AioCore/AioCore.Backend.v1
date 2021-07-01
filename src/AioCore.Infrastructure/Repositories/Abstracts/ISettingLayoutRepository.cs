using System;
using System.Threading.Tasks;
using AioCore.Domain.CoreEntities;

namespace AioCore.Infrastructure.Repositories.Abstracts
{
    public interface ISettingLayoutRepository : IRepository<SettingLayout>
    {
        Task<SettingLayout> GetAsync(Guid id);
    }
}