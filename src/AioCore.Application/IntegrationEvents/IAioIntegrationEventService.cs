using Package.EventBus.EventBus.Events;
using System;
using System.Threading.Tasks;

namespace AioCore.Application.IntegrationEvents
{
    public interface IAioIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);

        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}