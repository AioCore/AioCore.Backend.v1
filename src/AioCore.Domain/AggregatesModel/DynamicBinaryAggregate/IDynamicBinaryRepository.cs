using AioCore.Shared.Seedwork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AioCore.Domain.AggregatesModel.DynamicBinaryAggregate
{
    public interface IDynamicBinaryRepository : IRepository<DynamicBinary>
    {
        Task<DynamicBinary> GetAsync(Guid id);

        List<DynamicBinary> AddRange(IEnumerable<DynamicBinary> binaries);

        DynamicBinary Add(DynamicBinary binary);
    }
}