using System;
using System.Threading.Tasks;
using AioCore.Domain.CoreEntities;

namespace AioCore.Application.Repositories
{
    public interface ISettingLayoutRepository : IRepository<SettingLayout>
    {
        Task<SettingLayout> GetAsync(Guid id);
    }
}