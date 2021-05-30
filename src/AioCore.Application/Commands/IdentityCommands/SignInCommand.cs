using AioCore.Application.Responses.IdentityResponses;
using AioCore.Infrastructure;
using AioCore.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Package.Extensions;
using Package.Localization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        internal class Handler : IRequestHandler<SignInCommand, SignInResponse>
        {
            private readonly AioCoreContext _context;
            private readonly AppSettings _appSettings;
            private readonly IStringLocalizer<Localization> _localizer;

            public Handler(
                AioCoreContext context
                , IOptions<AppSettings> appSettings
                , IStringLocalizer<Localization> localizer)
            {
                _context = context;
                _appSettings = appSettings.Value;
                _localizer = localizer;
            }

            public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.SystemUsers
                    .Include(x => x.Tenant).ThenInclude(x => x.TenantApplications)
                    .Include(x => x.Policies).ThenInclude(x => x.Policy)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Account == request.Key || x.Email == request.Key, cancellationToken);

                if (user == null || !user.PasswordHash.Equals(request.Password.CreateMd5()))
                {
                    return new SignInResponse
                    {
                        Success = false,
                        Message = _localizer[Message.SignInMessageFail]
                    };
                }

                var policies = string.Join(";", user.Policies.Select(t => $"{t.Policy.Controller}|{t.Policy.Action}"));

                var apps = string.Join(";", user.Tenant.TenantApplications.Select(t => t.ApplicationId));

                var claims = new[]
                {
                    new Claim("email", user.Email),
                    new Claim("account", user.Account),
                    new Claim("id", user.Id.ToString()),
                    new Claim("tenant", user.TenantId.ToString()),
                    new Claim("schema", user.Tenant.Schema),
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
                    Success = false,
                    Message = _localizer[Message.SignInMessageSuccess],
                    Token = token
                };
            }
        }
    }
}