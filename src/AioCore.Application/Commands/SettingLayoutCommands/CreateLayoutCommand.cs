using AioCore.Application.Responses.SettingLayoutResponses;
using AioCore.Domain.AggregatesModel.SettingLayoutAggregate;
using MediatR;
using Package.AutoMapper;
using Package.Elasticsearch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.SettingLayoutCommands
{
    public class CreateLayoutCommand : IRequest<CreateLayoutResponse>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        internal class Handler : IRequestHandler<CreateLayoutCommand, CreateLayoutResponse>
        {
            private readonly ISettingLayoutRepository _settingLayoutRepository;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                ISettingLayoutRepository settingLayoutRepository,
                IElasticsearchService elasticsearchService)
            {
                _settingLayoutRepository = settingLayoutRepository ?? throw new ArgumentNullException(nameof(settingLayoutRepository));
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<CreateLayoutResponse> Handle(CreateLayoutCommand request, CancellationToken cancellationToken)
            {
                var item = _settingLayoutRepository.Add(new SettingLayout(request.Name, request.Description));
                await _settingLayoutRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.IndexAsync(item);
                return item.To<CreateLayoutResponse>();
            }
        }
    }
}