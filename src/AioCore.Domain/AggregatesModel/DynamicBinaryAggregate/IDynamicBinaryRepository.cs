using AioCore.Shared.Seedwork;
using System.Collections.Generic;

namespace AioCore.Domain.AggregatesModel.DynamicBinaryAggregate
{
    public interface IDynamicBinaryRepository : IRepository<DynamicBinary>
    {
        List<DynamicBinary> AddRange(IEnumerable<DynamicBinary> binaries);

        DynamicBinary Add(DynamicBinary binary);
    }
}