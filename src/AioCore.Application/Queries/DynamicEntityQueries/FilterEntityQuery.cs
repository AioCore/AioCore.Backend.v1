using AioCore.Shared.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Queries.DynamicEntityQueries
{
    public class FilterEntityQuery : IRequest<Pagination<object>>
    {
        internal class Handler : IRequestHandler<FilterEntityQuery, Pagination<object>>
        {
            public async Task<Pagination<object>> Handle(FilterEntityQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
