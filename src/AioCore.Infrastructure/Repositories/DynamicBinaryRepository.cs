using AioCore.Domain.AggregatesModel.DynamicBinaryAggregate;
using AioCore.Shared.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Repositories
{
    public class DynamicBinaryRepository : IDynamicBinaryRepository
    {
        private readonly AioCoreContext _context;

        public DynamicBinaryRepository(AioCoreContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<DynamicBinary> GetAsync(Guid id)
        {
            return await _context.DynamicBinaries.FindAsync(id);
        }

        public List<DynamicBinary> AddRange(IEnumerable<DynamicBinary> binaries)
        {
            return binaries.Select(binary => _context.DynamicBinaries.Add(binary).Entity).ToList();
        }

        public DynamicBinary Add(DynamicBinary binary)
        {
            return _context.DynamicBinaries.Add(binary).Entity;
        }
    }
}