using AioCore.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.DynamicAction;

namespace AioCore.Application.DynamicCommand
{
    public class DynamicPublisher : INotification
    {
        public DynamicActionModel ActionModel { get; set; }

        internal class Handler : INotificationHandler<DynamicPublisher>
        {
            private readonly ActionFactory _actionFactory;
            private readonly ILogger<DynamicPublisher> _logger;

            public Handler(ActionFactory actionFactory, ILogger<DynamicPublisher> logger)
            {
                _actionFactory = actionFactory;
                _logger = logger;
            }

            public async Task Handle(DynamicPublisher notification, CancellationToken cancellationToken)
            {
                try
                {
                    var processor = _actionFactory.GetProcessor(notification.ActionModel.ActionStep.StepType);
                    if (processor == null) return;
                    await processor.ExecuteAsync(notification.ActionModel, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "DynamicPublisher EXCEPTION");
                }
            }
        }
    }
}