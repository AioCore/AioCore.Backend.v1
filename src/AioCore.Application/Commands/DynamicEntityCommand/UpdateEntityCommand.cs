using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.DynamicEntityCommand
{
    public class UpdateEntityCommand : IRequest<UpdateEntityRespone>
    {
        public Guid EntityId { get; set; }
        public string Name { get; set; }
        public List<AttributeValueModel> AttributeValues { get; set; }

        internal class Handler : IRequestHandler<UpdateEntityCommand, UpdateEntityRespone>
        {
            public Handler()
            {

            }

            public async Task<UpdateEntityRespone> Handle(UpdateEntityCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

    }
}
