using AioCore.Application.Responses.IdentityResponses;
using AioCore.Application.UnitOfWorks;
using AioCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Package.DatabaseManagement;
using Package.Extensions;
using Package.Localization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.IdentityCommands
{
    public class SignInCommand : IRequest<SignInResponse>
    {
        public string Key { get; set; }

        public string Password { get; set; }

        public Guid TenantId { get; set; }

        internal class Handler : IRequestHandler<SignInCommand, SignInResponse>
        {
            private readonly IAioCoreUnitOfWork _context;
            private readonly AppSettings _appSettings;
            private readonly IStringLocalizer<Localization> _localizer;

            public Handler(
                  IAioCoreUnitOfWork context
                , IOptions<AppSettings> appSettings
                , IStringLocalizer<Localization> localizer)
            {
                _context = context;
                _appSettings = appSettings.Value;
                _localizer = localizer;
            }

            public async Task<Response<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.SystemUsers
                    .Include(x => x.Policies).ThenInclude(x => x.Policy)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Account == request.Key || x.Email == request.Key, cancellationToken);

                if (user == null || !user.PasswordHash.Equals(request.Password.CreateMd5()))
                {
                    return new Response<SignInResponse>
                    {
                        Success = false,
                        Status = HttpStatusCode.BadRequest,
                        Data = new SignInResponse
                        {
                            Message = _localizer[Message.SignInMessageFail]
                        }
                    };
                }

                var tenant = await _context.SystemTenants
                    .Where(t => t.Id == request.TenantId && t.Users.Any(x => x.Id == user.Id))
                    .Include(t => t.TenantApplications)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);

                if (tenant == null)
                {
                    return new Response<SignInResponse>
                    {
                        Success = false,
                        Status = HttpStatusCode.BadRequest,
                        Data = new SignInResponse
                        {
                            Message = _localizer[Message.SignInMessageFail]
                        }
                    };
                }

                var policies = string.Join(";", user.Policies.Select(t => $"{t.Policy.Controller}|{t.Policy.Action}"));

                var apps = string.Join(";", tenant.TenantApplications.Select(t => t.ApplicationId));

                var dbInfo = DatabaseInfo.Parse(tenant.DatabaseInfo);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("id", user.Id.ToString()),
                    new Claim("email", user.Email),
                    new Claim("account", user.Account),
                    new Claim("tenant", tenant.Id.ToString()),
                    new Claim("schema", dbInfo?.Schema),
                    new Claim("apps", apps),
                    new Claim("policies", policies),
                    new Claim("groups", ""),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Tokens.Key));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenNotEncrypt = new JwtSecurityToken(_appSettings.Tokens.Issuer, _appSettings.Tokens.Issuer,
                    claims,
                    expires: DateTime.UtcNow.AddHours(_appSettings.ExpiredTime),
                    signingCredentials: credentials);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenNotEncrypt);
                return new SignInResponse
                {
                    Message = _localizer[Message.SignInMessageSuccess],
                    Token = token
                };
            }
        }
    }
}