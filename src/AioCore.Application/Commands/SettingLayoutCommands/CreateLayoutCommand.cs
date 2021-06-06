using AioCore.Application.Responses.SettingLayoutResponses;
using MediatR;
using Package.AutoMapper;
using Package.Elasticsearch;
using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Domain.SettingAggregatesModel.SettingLayoutAggregate;
using AioCore.Application.UnitOfWorks;

namespace AioCore.Application.Commands.SettingLayoutCommands
{
    public class CreateLayoutCommand : IRequest<CreateLayoutResponse>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        internal class Handler : IRequestHandler<CreateLayoutCommand, CreateLayoutResponse>
        {
            private readonly IAioCoreUnitOfWork _context;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                IAioCoreUnitOfWork context,
                IElasticsearchService elasticsearchService)
            {
                _context = context;
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<CreateLayoutResponse> Handle(CreateLayoutCommand request, CancellationToken cancellationToken)
            {
                var item = _context.SettingLayouts.Add(new SettingLayout(request.Name, request.Description));
                await _context.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.IndexAsync(item);
                return item.MapTo<CreateLayoutResponse>();
            }
        }
    }
}