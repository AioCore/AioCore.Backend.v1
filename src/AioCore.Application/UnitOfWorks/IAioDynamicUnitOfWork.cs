using AioCore.Application.Repositories;
using AioCore.Domain.DynamicEntities;

namespace AioCore.Application.UnitOfWorks
{
    public interface IAioDynamicUnitOfWork : IUnitOfWork
    {
        IRepository<DynamicEntity> DynamicEntities { get; }
        IRepository<DynamicAttribute> DynamicAttributes { get; }

        IRepository<DynamicDateValue> DynamicDateValues { get; }
        IRepository<DynamicFloatValue> DynamicFloatValues { get; }
        IRepository<DynamicGuidValue> DynamicGuidValues { get; }
        IRepository<DynamicIntegerValue> DynamicIntegerValues { get; }
        IRepository<DynamicStringValue> DynamicStringValues { get; }
    }
}
