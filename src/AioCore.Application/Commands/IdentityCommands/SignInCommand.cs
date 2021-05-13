using AioCore.Application.Responses.IdentityResponses;
using AioCore.Infrastructure;
using AioCore.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Package.Extensions;
using Package.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.IdentityCommands
{
    public class SignInCommand : IRequest<SignInResponse>
    {
        public string Key { get; set; }

        public string Password { get; set; }

        internal class Handler : IRequestHandler<SignInCommand, SignInResponse>
        {
            private readonly AioCoreContext _context;
            private readonly IStringLocalizer<Localization> _localizer;

            public Handler(AioCoreContext context, IStringLocalizer<Localization> localizer)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            }

            public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
            {
                var res = await _context.SecurityUsers.FirstOrDefaultAsync(
                    x => x.Account == request.Key || x.Email == request.Key, cancellationToken);
                var message = res.PasswordHash.Equals(request.Password.CreateMd5())
                    ? _localizer[Message.SignInMessageSuccess]
                    : _localizer[Message.SignInMessageFail];
                return new SignInResponse { Message = message };
            }
        }
    }
}