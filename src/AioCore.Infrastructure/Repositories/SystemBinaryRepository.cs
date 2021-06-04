using AioCore.Domain.SystemAggregatesModel.SystemBinaryAggregate;
using AioCore.Shared.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Repositories
{
    public class SystemBinaryRepository : ISystemBinaryRepository
    {
        private readonly AioDynamicContext _context;

        public SystemBinaryRepository(AioDynamicContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<SystemBinary> GetAsync(Guid id)
        {
            return await _context.DynamicBinaries.FindAsync(id);
        }

        public List<SystemBinary> AddRange(IEnumerable<SystemBinary> binaries)
        {
            return binaries.Select(binary => _context.DynamicBinaries.Add(binary).Entity).ToList();
        }

        public SystemBinary Add(SystemBinary binary)
        {
            return _context.DynamicBinaries.Add(binary).Entity;
        }
    }
}