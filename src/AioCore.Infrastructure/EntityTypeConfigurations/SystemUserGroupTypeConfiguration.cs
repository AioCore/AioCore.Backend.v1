using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemUserGroupTypeConfiguration : EntityTypeConfiguration<SystemUserGroup>
    {
        public override void Config(EntityTypeBuilder<SystemUserGroup> builder)
        {
        }
    }
}