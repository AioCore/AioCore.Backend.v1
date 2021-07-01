using AioCore.Domain.DynamicEntities;
using AioCore.Infrastructure.DbContexts;
using AioCore.Infrastructure.Repositories.Abstracts;
using AioCore.Infrastructure.UnitOfWorks.Abstracts;

namespace AioCore.Infrastructure.UnitOfWorks
{
    public class AioDynamicUnitOfWork : UnitOfWork, IAioDynamicUnitOfWork
    {
        public IRepository<DynamicEntity> DynamicEntities { get; set; }
        public IRepository<DynamicAttribute> DynamicAttributes { get; set; }
        public IRepository<DynamicDateValue> DynamicDateValues { get; set; }
        public IRepository<DynamicFloatValue> DynamicFloatValues { get; set; }
        public IRepository<DynamicGuidValue> DynamicGuidValues { get; set; }
        public IRepository<DynamicIntegerValue> DynamicIntegerValues { get; set; }
        public IRepository<DynamicStringValue> DynamicStringValues { get; set; }

        public AioDynamicUnitOfWork(AioDynamicContext dbContext) : base(dbContext)
        {
        }
    }
}