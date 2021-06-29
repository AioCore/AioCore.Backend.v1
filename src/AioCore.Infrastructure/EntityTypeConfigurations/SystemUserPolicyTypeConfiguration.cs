using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemUserPolicyTypeConfiguration : EntityTypeConfiguration<SystemUserPolicy>
    {
        public override void Config(EntityTypeBuilder<SystemUserPolicy> builder)
        {
        }
    }
}