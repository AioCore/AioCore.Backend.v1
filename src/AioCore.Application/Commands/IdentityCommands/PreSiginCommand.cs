using AioCore.Application.Responses.IdentityResponses;
using AioCore.Application.UnitOfWorks;
using AioCore.Mediator;
using AioCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Package.Extensions;
using Package.Localization;
using System.Linq;
using System.Net;
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

            public async Task<Response<PreSigninRespone>> Handle(PreSiginCommand request, CancellationToken cancellationToken)
            {
                var user = await _coreUnitOfWork.SystemUsers
                       .Where(x => x.Account == request.Key || x.Email == request.Key)
                       .Include(t => t.Tenants)
                       .FirstOrDefaultAsync(cancellationToken);

                if (user == null || !user.PasswordHash.Equals(request.Password.CreateMd5()))
                {
                    return new Response<PreSigninRespone>
                    {
                        Success = false,
                        Status = HttpStatusCode.BadRequest,
                        Data = new PreSigninRespone
                        {
                            Message = _localizer[Message.SignInMessageFail]
                        }
                    };
                }

                return new PreSigninRespone
                {
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
