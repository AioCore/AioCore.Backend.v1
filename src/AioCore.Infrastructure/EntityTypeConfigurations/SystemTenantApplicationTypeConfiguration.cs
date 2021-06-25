using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemTenantApplicationTypeConfiguration : EntityTypeConfiguration<SystemTenantApplication>
    {
        public override void Config(EntityTypeBuilder<SystemTenantApplication> builder)
        {
        }
    }
}