using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemPolicyTypeConfiguration : EntityTypeConfiguration<SystemPolicy>
    {
        public override void Config(EntityTypeBuilder<SystemPolicy> builder)
        {
        }
    }
}