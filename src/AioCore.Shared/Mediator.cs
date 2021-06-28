namespace AioCore.Shared
{
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
