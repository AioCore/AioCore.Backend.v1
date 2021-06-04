using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.SystemAggregatesModel.SystemBinaryAggregate
{
    public interface ISystemBinaryRepository : IRepository<SystemBinary>
    {
        Task<SystemBinary> GetAsync(Guid id);

        List<SystemBinary> AddRange(IEnumerable<SystemBinary> binaries);

        SystemBinary Add(SystemBinary binary);
    }
}