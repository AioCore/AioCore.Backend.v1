using AioCore.Application.UnitOfWorks;
using AioCore.Shared;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.DynamicCommand
{
    public  class DynamicCommand : IRequest<Response<object>>
    {
        public Guid ComponentId { get; set; }

        internal class Handler : IRequestHandler<DynamicCommand, Response<object>>
        {
            private readonly IAioCoreUnitOfWork _coreUnitOfWork;

            public Handler(IAioCoreUnitOfWork coreUnitOfWork)
            {
                _coreUnitOfWork = coreUnitOfWork;
            }

            public async Task<Response<object>> Handle(DynamicCommand request, CancellationToken cancellationToken)
            {
                var component = await _coreUnitOfWork.SettingComponents.FindAsync(new object[] { request.ComponentId }, cancellationToken: cancellationToken);



                return await Task.FromResult(new Response<object>());
            }
        }
    }
}
