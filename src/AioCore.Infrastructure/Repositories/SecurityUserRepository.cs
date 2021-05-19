using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using AioCore.Shared.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Repositories
{
    public class SecurityUserRepository : ISecurityUserRepository
    {
        private readonly AioCoreContext _context;

        public SecurityUserRepository(AioCoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<SecurityUser> GetAsync(Guid id)
        {
            return await _context.SecurityUsers.FindAsync(id);
        }

        public SecurityUser Add(SecurityUser user)
        {
            return _context.SecurityUsers.Add(user).Entity;
        }

        public void Update(SecurityUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var user = _context.SecurityUsers.Find(id);
            _context.Entry(user).State = EntityState.Deleted;
        }
    }
}