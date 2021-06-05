using AioCore.Application.Repositories;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.DynamicAggregatesModel;
using AioCore.Infrastructure.Repositories;

namespace AioCore.Infrastructure.UnitOfWorks
{
    public class AioDynamicUnitOfWork : UnitOfWork, IAioDynamicUnitOfWork
    {
        public IRepository<DynamicEntity> DynamicEntities { get; }
        public IRepository<DynamicAttribute> DynamicAttributes { get; }
        public IRepository<DynamicDateValue> DynamicDateValues { get; }
        public IRepository<DynamicFloatValue> DynamicFloatValues { get; }
        public IRepository<DynamicGuidValue> DynamicGuidValues { get; }
        public IRepository<DynamicIntegerValue> DynamicIntegerValues { get; }
        public IRepository<DynamicStringValue> DynamicStringValues { get; }

        public AioDynamicUnitOfWork(AioDynamicContext dbContext) : base(dbContext)
        {
            DynamicEntities = new RepositoryImpl<DynamicEntity>(dbContext);
            DynamicAttributes = new RepositoryImpl<DynamicAttribute>(dbContext);
            DynamicDateValues = new RepositoryImpl<DynamicDateValue>(dbContext);
            DynamicFloatValues = new RepositoryImpl<DynamicFloatValue>(dbContext);
            DynamicGuidValues = new RepositoryImpl<DynamicGuidValue>(dbContext);
            DynamicIntegerValues = new RepositoryImpl<DynamicIntegerValue>(dbContext);
            DynamicStringValues = new RepositoryImpl<DynamicStringValue>(dbContext);
        }
    }
}
