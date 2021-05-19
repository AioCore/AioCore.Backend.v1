using AioCore.Application.Responses.SettingLayoutResponses;
using AioCore.Domain.AggregatesModel.SettingLayoutAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.SettingLayoutCommands
{
    public class UpdateLayoutCommand : IRequest<UpdateLayoutResponse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        internal class Handler : IRequestHandler<UpdateLayoutCommand, UpdateLayoutResponse>
        {
            private readonly ISettingLayoutRepository _layoutRepository;

            public Handler(
                ISettingLayoutRepository layoutRepository)
            {
                _layoutRepository = layoutRepository ?? throw new ArgumentNullException(nameof(layoutRepository));
            }

            public async Task<UpdateLayoutResponse> Handle(UpdateLayoutCommand request, CancellationToken cancellationToken)
            {
                var layout = await _layoutRepository.GetAsync(request.Id);
                throw new System.NotImplementedException();
            }
        }
    }
}