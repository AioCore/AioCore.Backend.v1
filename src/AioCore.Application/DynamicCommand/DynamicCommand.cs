using AioCore.Application.UnitOfWorks;
using AioCore.Domain.Models;
using AioCore.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

                var query = from t1 in _coreUnitOfWork.SettingComponents.Where(t=>t.ComponentType == ComponentType.Action)
                            join t2 in _coreUnitOfWork.SettingActions on t1.ParentId equals t2.Id
                            select t2;
                var actions = await query
                    .Include(t => t.SettingActionSteps)
                    .ToListAsync(cancellationToken);

                return await Task.FromResult(new Response<object>());
            }
        }
    }
}
