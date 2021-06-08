using AioCore.Application.Responses.IdentityResponses;
using AioCore.Shared;
using MediatR;
using Microsoft.Extensions.Localization;
using Package.Elasticsearch;
using Package.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;
using AioCore.Application.UnitOfWorks;
using Package.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AioCore.Application.Commands.IdentityCommands
{
    public class SignUpCommand : IRequest<SignUpResponse>
    {
        public string Name { get; set; }

        public string Account { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public Guid TenantId { get; set; }

        internal class Handler : IRequestHandler<SignUpCommand, SignUpResponse>
        {
            private readonly IAioCoreUnitOfWork _aioCoreUnitOfWork;
            private readonly IStringLocalizer<Localization> _localizer;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                 IAioCoreUnitOfWork context,
                 IStringLocalizer<Localization> localizer,
                 IElasticsearchService elasticsearchService)
            {
                _aioCoreUnitOfWork = context;
                _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    //check if tenant doesn't exists 
                    if (!(await _aioCoreUnitOfWork.SystemTenants.AnyAsync(t => t.Id == request.TenantId, cancellationToken)))
                    {
                        return new SignUpResponse { Message = _localizer[Message.SignUpMessageTenantNotExists] };
                    }

                    //check if account has existed
                    if (await _aioCoreUnitOfWork.SystemUsers.AnyAsync(t => t.Account == request.Account && t.TenantId == request.TenantId, cancellationToken))
                    {
                        return new SignUpResponse { Message = string.Format(_localizer[Message.SignUpMessageAccountExisted], request.Account) };
                    }

                    //check if account has existed
                    if (await _aioCoreUnitOfWork.SystemUsers.AnyAsync(t => t.Email == request.Email && t.TenantId == request.TenantId, cancellationToken))
                    {
                        return new SignUpResponse { Message = string.Format(_localizer[Message.SignUpMessageEmailExisted], request.Email) };
                    }

                    var user = _aioCoreUnitOfWork.SystemUsers.Add(new SystemUser
                    {
                        Name = request.Name,
                        Account = request.Account,
                        Email = request.Email,
                        PasswordHash = request.Password.CreateMd5(),
                        TenantId = request.TenantId
                    });
                    await _aioCoreUnitOfWork.SaveChangesAsync(cancellationToken);
                    await _elasticsearchService.IndexAsync(user);
                    return new SignUpResponse { Message = _localizer[Message.SignUpMessageSuccess] };
                }
                catch (Exception e)
                {
                    throw new ApplicationException(_localizer[Message.SignUpMessageFail, e.Message]);
                }
            }
        }
    }
}