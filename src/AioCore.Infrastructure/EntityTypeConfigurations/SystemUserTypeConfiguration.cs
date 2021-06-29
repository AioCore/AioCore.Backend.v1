using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemUserTypeConfiguration : EntityTypeConfiguration<SystemUser>
    {
        public override void Config(EntityTypeBuilder<SystemUser> builder)
        {
            builder.HasMany(x => x.Tenants)
                .WithMany(x => x.Users)
                .UsingEntity<SystemUserTenant>(
                    x => x.HasOne(x => x.Tenant).WithMany().HasForeignKey(x => x.TenantId),
                    x => x.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId)
                );
        }
    }
}