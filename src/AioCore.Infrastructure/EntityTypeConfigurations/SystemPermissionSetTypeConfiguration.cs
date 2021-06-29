using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemPermissionSetTypeConfiguration : EntityTypeConfiguration<SystemPermissionSet>
    {
        public override void Config(EntityTypeBuilder<SystemPermissionSet> builder)
        {
        }
    }
}