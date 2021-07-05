using AioCore.Domain.CoreEntities;
using AioCore.Infrastructure.DbContexts;
using System;
using System.Threading.Tasks;
using AioCore.Application.Repositories;

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