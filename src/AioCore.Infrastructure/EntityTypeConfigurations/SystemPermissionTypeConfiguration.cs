using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemPermissionTypeConfiguration : EntityTypeConfiguration<SystemPermission>
    {
        public override void Config(EntityTypeBuilder<SystemPermission> builder)
        {
        }
    }
}