using AioCore.Domain.AggregatesModel.DynamicAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicEntityTypeConfiguration : IEntityTypeConfiguration<DynamicEntity>
    {
        public void Configure(EntityTypeBuilder<DynamicEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}