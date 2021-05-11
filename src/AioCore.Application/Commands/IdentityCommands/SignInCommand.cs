using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.IdentityCommands
{
    public class SignInCommand : IRequest<bool>
    {
        public string Key { get; set; }

        public string Password { get; set; }

        internal class Handler : IRequestHandler<SignInCommand, bool>
        {
            public Task<bool> Handle(SignInCommand request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}