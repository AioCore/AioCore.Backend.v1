using AioCore.Domain.DynamicEntities;
using AioCore.Infrastructure.Repositories.Abstracts;

namespace AioCore.Infrastructure.UnitOfWorks.Abstracts
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