using AioCore.Shared.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;

namespace AioCore.Infrastructure.Repositories
{
    public class SystemUserRepository : ISystemUserRepository
    {
        private readonly AioCoreContext _context;

        public SystemUserRepository(AioCoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<SystemUser> GetAsync(Guid id)
        {
            return await _context.SystemUsers.FindAsync(id);
        }

        public SystemUser Add(SystemUser user)
        {
            return _context.SystemUsers.Add(user).Entity;
        }

        public void Update(SystemUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var user = _context.SystemUsers.Find(id);
            _context.Entry(user).State = EntityState.Deleted;
        }
    }
}