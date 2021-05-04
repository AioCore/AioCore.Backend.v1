using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SecurityUserTypeConfiguration : IEntityTypeConfiguration<SecurityUser>
    {
        public void Configure(EntityTypeBuilder<SecurityUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}