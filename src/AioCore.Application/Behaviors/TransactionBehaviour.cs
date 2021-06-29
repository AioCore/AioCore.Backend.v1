using AioCore.Application.IntegrationEvents;
using AioCore.Application.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Package.EventBus.EventBus.Extensions;
using Serilog.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Behaviors
{
    public class TransactionBehaviour<TRequest, TResponse> //: IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly IAioCoreUnitOfWork _dbContext;
        private readonly IAioIntegrationEventService _orderingIntegrationEventService;

        public TransactionBehaviour(IAioCoreUnitOfWork dbContext,
            IAioIntegrationEventService orderingIntegrationEventService,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(_dbContext));
            _orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentException(nameof(orderingIntegrationEventService));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

            try
            {
                if (_dbContext.GetCurrentTransaction() is not null)
                {
                    return await next();
                }

                var strategy = _dbContext.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    await using (var transaction = await _dbContext.BeginTransactionAsync())
                    using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                    {
                        _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                        response = await next();

                        _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                        await _dbContext.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }

                    await _orderingIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

                throw;
            }
        }
    }
}