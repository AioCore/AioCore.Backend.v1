using AioCore.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using AioCore.Domain;
using AioCore.Infrastructure.Services.Abstracts;

namespace AioCore.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DomainEventService> _logger;
        private static readonly ConcurrentDictionary<Type, Type> _domainEventTypes = new ConcurrentDictionary<Type, Type>();

        public DomainEventService(IMediator mediator, ILogger<DomainEventService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
        }

        private static INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        {
            var type = _domainEventTypes.GetOrAdd(domainEvent.GetType(), type =>
            {
                return typeof(DomainEventNotification<>).MakeGenericType(type);
            });

            return (INotification)Activator.CreateInstance(type, domainEvent);
        }
    }
}