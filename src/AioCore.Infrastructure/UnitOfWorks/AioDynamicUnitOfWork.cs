﻿using AioCore.Application.Repositories;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.DynamicAggregatesModel;

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