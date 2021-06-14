using AioCore.Application.Responses.IdentityResponses;
using AioCore.Application.UnitOfWorks;
using AioCore.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Package.Extensions;
using Package.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.IdentityCommands
{
    public class PreSiginCommand : IRequest<PreSigninRespone>
    {
        public string Key { get; set; }

        public string Password { get; set; }

        internal class Handler : IRequestHandler<PreSiginCommand, PreSigninRespone>
        {
            private readonly IAioCoreUnitOfWork _coreUnitOfWork;
            private readonly IStringLocalizer<Localization> _localizer;

            public Handler(
                  IAioCoreUnitOfWork coreUnitOfWork
                , IStringLocalizer<Localization> localizer)
            {
                _coreUnitOfWork = coreUnitOfWork;
                _localizer = localizer;
            }

            public async Task<PreSigninRespone> Handle(PreSiginCommand request, CancellationToken cancellationToken)
            {
                var user = await _coreUnitOfWork.SystemUsers
                       .Where(x => x.Account == request.Key || x.Email == request.Key)
                       .Include(t => t.Tenants)
                       .FirstOrDefaultAsync(cancellationToken);

                if (user == null || !user.PasswordHash.Equals(request.Password.CreateMd5()))
                {
                    return new PreSigninRespone
                    {
                        Success = false,
                        Message = _localizer[Message.SignInMessageFail]
                    };
                }

                return new PreSigninRespone
                {
                    Success = true,
                    Key = request.Key,
                    Password = request.Password,
                    Tenants = user.Tenants.Select(t => new TenantInfoRespone
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).ToList()
                };
            }
        }
    }
}
