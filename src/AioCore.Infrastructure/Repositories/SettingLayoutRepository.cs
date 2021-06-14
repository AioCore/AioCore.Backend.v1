using AioCore.Application.Repositories;
using AioCore.Domain.SettingAggregatesModel.SettingLayoutAggregate;
using AioCore.Infrastructure.DbContexts;
using System;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Repositories
{
    public class SettingLayoutRepository : Repository<SettingLayout>, ISettingLayoutRepository
    {
        private readonly AioCoreContext _context;

        public SettingLayoutRepository(AioCoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SettingLayout> GetAsync(Guid id)
        {
            return await _context.SettingLayouts.FindAsync(id);
        }
    }
}