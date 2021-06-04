using AioCore.Domain.SystemAggregatesModel.SystemGroupAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemGroupTypeConfiguration : IEntityTypeConfiguration<SystemGroup>
    {
        public void Configure(EntityTypeBuilder<SystemGroup> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Parent);
            builder.Ignore(x => x.Root);
        }
    }
}