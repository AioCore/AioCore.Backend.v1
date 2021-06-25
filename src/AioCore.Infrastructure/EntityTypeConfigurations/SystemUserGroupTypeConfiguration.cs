using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemUserGroupTypeConfiguration : IEntityTypeConfiguration<SystemUserGroup>
    {
        public void Configure(EntityTypeBuilder<SystemUserGroup> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}