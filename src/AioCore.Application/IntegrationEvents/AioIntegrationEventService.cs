using AioCore.Application.UnitOfWorks;
using Microsoft.Extensions.Logging;
using Package.EventBus.EventBus.Abstractions;
using Package.EventBus.EventBus.Events;
using Package.EventBus.IntegrationEventLogEF;
using Package.EventBus.IntegrationEventLogEF.Services;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace AioCore.Application.IntegrationEvents
{
    public class AioIntegrationEventService : IAioIntegrationEventService
    {
        private readonly IEventBus _eventBus;
        private readonly IAioCoreUnitOfWork _orderingContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<AioIntegrationEventService> _logger;
        private readonly string _appName;
        private readonly string _nameSpace = typeof(AioIntegrationEventService).Namespace;

        public AioIntegrationEventService(IEventBus eventBus,
            IAioCoreUnitOfWork orderingContext,
            IntegrationEventLogContext eventLogContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            ILogger<AioIntegrationEventService> logger)
        {
            _orderingContext = orderingContext ?? throw new ArgumentNullException(nameof(orderingContext));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = integrationEventLogServiceFactory(_orderingContext.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appName = _nameSpace?.Substring(_nameSpace.LastIndexOf('.', _nameSpace.LastIndexOf('.') - 1) + 1);
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvt in pendingLogEvents)
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", logEvt.EventId, _appName, logEvt.IntegrationEvent);

                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    _eventBus.Publish(logEvt.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", logEvt.EventId, _appName);

                    await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

            await _eventLogService.SaveEventAsync(evt, _orderingContext.GetCurrentTransaction());
        }
    }
}