using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Mediator
{
    internal class Mediator : MediatR.Mediator
    {
        private readonly Func<IEnumerable<Func<INotification, CancellationToken, Task>>, INotification, CancellationToken, Task> _publish;

        public Mediator(
              ServiceFactory serviceFactory
            , Func<IEnumerable<Func<INotification, CancellationToken, Task>>, INotification, CancellationToken, Task> publish
        ) : base(serviceFactory)
        {
            _publish = publish;
        }

        protected override Task PublishCore(IEnumerable<Func<INotification, CancellationToken, Task>> allHandlers, INotification notification, CancellationToken cancellationToken)
        {
            return _publish(allHandlers, notification, cancellationToken);
        }
    }

    public interface IRequest : MediatR.IRequest<Response>
    {

    }

    public interface IRequestHandler<in TRequest> : MediatR.IRequestHandler<TRequest, Response>
        where TRequest : MediatR.IRequest<Response>
    {

    }

    public interface IRequest<TResponse> : MediatR.IRequest<Response<TResponse>>
    {
    }

    public interface IRequestHandler<in TRequest, TResponse> : MediatR.IRequestHandler<TRequest, Response<TResponse>>
        where TRequest : MediatR.IRequest<Response<TResponse>>
    {

    }
}
