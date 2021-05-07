using AioCore.Domain.AggregatesModel.DynamicBinaryAggregate;
using AioCore.Shared.Seedwork;
using System.Collections.Generic;
using System.Linq;

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