using AioCore.Application.Responses.SettingLayoutResponses;
using AioCore.Domain.AggregatesModel.SettingLayoutAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Queries.SettingLayoutQueries
{
    public class GetLayoutQuery : IRequest<GetLayoutResponse>
    {
        public Guid Id { get; private set; }

        public void MergeParams(Guid id)
        {
            Id = id;
        }

        internal class Handler : IRequestHandler<GetLayoutQuery, GetLayoutResponse>
        {
            private readonly ISettingLayoutRepository _settingLayoutRepository;

            public Handler(ISettingLayoutRepository settingLayoutRepository)
            {
                _settingLayoutRepository = settingLayoutRepository ?? throw new ArgumentNullException(nameof(settingLayoutRepository));
            }

            public async Task<GetLayoutResponse> Handle(GetLayoutQuery request, CancellationToken cancellationToken)
            {
                var layout = await _settingLayoutRepository.GetAsync(request.Id);
                return new GetLayoutResponse(layout.Id, layout.Name);
            }
        }
    }
}