using AioCore.Application;
using AioCore.Application.Services;
using AioCore.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.DbContexts
{
    public static class ContextExtension
    {
        public static async Task<int> SaveEntitiesAsync(this DbContext context, IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
        {
            var dateTime = serviceProvider.GetRequiredService<IDateTime>();
            var currentUser = serviceProvider.GetRequiredService<ICurrentUser>();
            foreach (var entry in context.ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = currentUser.UserId;
                        entry.Entity.CreatedDate = dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = currentUser.UserId;
                        entry.Entity.UpdatedDate = dateTime.Now;
                        break;
                }
            }
            var result = await context.SaveChangesAsync(cancellationToken);
            await DispatchDomainEventsAsync(context, serviceProvider);
            return result;
        }

        private static async Task DispatchDomainEventsAsync(DbContext context, IServiceProvider serviceProvider)
        {
            var domainEventService = serviceProvider.GetRequiredService<IDomainEventService>();

            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents?.Any() == true)
                .ToList();

            foreach (var domainEvent in domainEntities.SelectMany(x => x.Entity.DomainEvents))
            {
                await domainEventService.Publish(domainEvent);
            }

            foreach (var entiy in domainEntities)
            {
                entiy.Entity.ClearDomainEvents();
            }
        }
    }
}
