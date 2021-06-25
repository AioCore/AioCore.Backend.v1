using AioCore.Application.Responses.SettingFeatureResponses;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.SettingFeatureCommands
{
    public class CreateFeatureCommand : IRequest<CreateFeatureResponse>
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public Guid PayoutId { get; set; }

        internal class Handler : IRequestHandler<CreateFeatureCommand, CreateFeatureResponse>
        {
            public async Task<CreateFeatureResponse> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}