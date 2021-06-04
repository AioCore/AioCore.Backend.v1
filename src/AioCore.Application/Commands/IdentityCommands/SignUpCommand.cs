﻿using AioCore.Application.Responses.IdentityResponses;
using AioCore.Shared;
using MediatR;
using Microsoft.Extensions.Localization;
using Package.Elasticsearch;
using Package.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;

namespace AioCore.Application.Commands.IdentityCommands
{
    public class SignUpCommand : IRequest<SignUpResponse>
    {
        public string Name { get; set; }

        public string Account { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        internal class Handler : IRequestHandler<SignUpCommand, SignUpResponse>
        {
            private readonly IStringLocalizer<Localization> _localizer;
            private readonly ISystemUserRepository _systemUserRepository;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                ISystemUserRepository systemUserRepository,
                IStringLocalizer<Localization> localizer,
                IElasticsearchService elasticsearchService)
            {
                _systemUserRepository = systemUserRepository ?? throw new ArgumentNullException(nameof(systemUserRepository));
                _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (!request.Password.Equals(request.ConfirmPassword))
                        return new SignUpResponse { Message = _localizer[Message.SignUpMessagePasswordNotMatch] };
                    var user = _systemUserRepository.Add(new SystemUser(request.Name, request.Account,
                        request.Email, request.Password));
                    await _systemUserRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
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