using AioCore.Domain.AggregatesModel.SettingLayoutAggregate;
using AioCore.Shared.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Repositories
{
    public class SettingLayoutRepository : ISettingLayoutRepository
    {
        private readonly AioCoreContext _context;

        public SettingLayoutRepository(AioCoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<SettingLayout> GetAsync(Guid id)
        {
            return await _context.SettingLayouts.FindAsync(id);
        }

        public SettingLayout Add(SettingLayout layout)
        {
            return _context.SettingLayouts.Add(layout).Entity;
        }

        public void Update(SettingLayout layout)
        {
            _context.Entry(layout).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var layout = _context.SettingLayouts.Find(id);
            _context.Entry(layout).State = EntityState.Deleted;
        }
    }
}