using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemGroupTypeConfiguration : EntityTypeConfiguration<SystemGroup>
    {
        public override void Config(EntityTypeBuilder<SystemGroup> builder)
        {
            builder.Ignore(x => x.Parent);
            builder.Ignore(x => x.Root);
        }
    }
}