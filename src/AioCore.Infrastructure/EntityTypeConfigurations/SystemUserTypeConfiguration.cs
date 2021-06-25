using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemUserTypeConfiguration : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
            builder.HasMany(x => x.Tenants)
                .WithMany(x => x.Users)
                .UsingEntity<SystemUserTenant>(
                    x => x.HasOne(x => x.Tenant).WithMany().HasForeignKey(x => x.TenantId),
                    x => x.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId)
                );
        }
    }
}