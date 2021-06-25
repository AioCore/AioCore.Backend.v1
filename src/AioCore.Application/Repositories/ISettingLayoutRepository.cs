using AioCore.Domain.CoreEntities;
using System;
using System.Threading.Tasks;

namespace AioCore.Application.Repositories
{
    public interface ISettingLayoutRepository : IRepository<SettingLayout>
    {
        Task<SettingLayout> GetAsync(Guid id);
    }
}