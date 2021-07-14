using AioCore.Application.Responses.IdentityResponses;
using AioCore.Application.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Package.Extensions;
using Package.Mediator;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.IdentityCommands
{
    public class PreSigInCommand : IRequest<PreSigninRespone>
    {
        public string Key { get; set; }

        public string Password { get; set; }

        internal class Handler : IRequestHandler<PreSigInCommand, PreSigninRespone>
        {
            private readonly IAioCoreUnitOfWork _coreUnitOfWork;

            public Handler(IAioCoreUnitOfWork coreUnitOfWork)
            {
                _coreUnitOfWork = coreUnitOfWork;
            }

            public async Task<Response<PreSigninRespone>> Handle(PreSigInCommand request, CancellationToken cancellationToken)
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
                            Message = "Đăng nhập thất bại"
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