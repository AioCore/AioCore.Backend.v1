using AioCore.Application.Responses.SettingFeatureResponses;
using MediatR;
using Package.Elasticsearch;
using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Domain.SettingAggregatesModel.SettingFeatureAggregate;

namespace AioCore.Application.Commands.SettingFeatureCommands
{
    public class CreateFeatureCommand : IRequest<CreateFeatureResponse>
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public Guid PayoutId { get; set; }

        internal class Handler : IRequestHandler<CreateFeatureCommand, CreateFeatureResponse>
        {
            private readonly IElasticsearchService _elasticsearchService;
            private readonly ISettingFeatureRepository _settingFeatureRepository;

            public Handler(IElasticsearchService elasticsearchService, ISettingFeatureRepository settingFeatureRepository)
            {
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
                _settingFeatureRepository = settingFeatureRepository ?? throw new ArgumentNullException(nameof(settingFeatureRepository));
            }

            public async Task<CreateFeatureResponse> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}