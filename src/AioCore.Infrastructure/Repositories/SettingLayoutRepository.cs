using AioCore.Domain.AggregatesModel.SettingLayoutAggregate;
using AioCore.Shared.Seedwork;
using System;

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
        public SettingLayout Add(SettingLayout layout)
        {
            throw new NotImplementedException();
        }

        public void Update(SettingLayout layout)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}