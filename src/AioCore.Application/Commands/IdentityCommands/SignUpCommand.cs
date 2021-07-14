using AioCore.Application.Responses.IdentityResponses;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.CoreEntities;
using AioCore.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Package.Elasticsearch;
using Package.Extensions;
using Package.Mediator;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
            private readonly IAioCoreUnitOfWork _aioCoreUnitOfWork;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                 IAioCoreUnitOfWork context,
                 IElasticsearchService elasticsearchService)
            {
                _aioCoreUnitOfWork = context;
                _elasticsearchService = elasticsearchService;
            }

            public async Task<Response<SignUpResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    //check if account has existed
                    if (await _aioCoreUnitOfWork.SystemUsers.AnyAsync(t => t.Account == request.Account, cancellationToken))
                    {
                        return new Response<SignUpResponse>
                        {
                            Success = false,
                            Status = HttpStatusCode.BadRequest,
                            Data = new SignUpResponse
                            {
                                Message = string.Format(Messages.SignUpAccountExisted, request.Account)
                            }
                        };
                    }

                    //check if account has existed
                    if (await _aioCoreUnitOfWork.SystemUsers.AnyAsync(t => t.Email == request.Email, cancellationToken))
                    {
                        return new Response<SignUpResponse>
                        {
                            Success = false,
                            Status = HttpStatusCode.BadRequest,
                            Data = new SignUpResponse
                            {
                                Message = string.Format(Messages.SignUpEmailExisted, request.Email)
                            }
                        };
                    }

                    var user = await _aioCoreUnitOfWork.SystemUsers.AddAsync(new SystemUser
                    {
                        Name = request.Name,
                        Account = request.Account,
                        Email = request.Email,
                        PasswordHash = request.Password.CreateMd5(),
                    }, cancellationToken);
                    await _aioCoreUnitOfWork.SaveChangesAsync(cancellationToken);
                    await _elasticsearchService.IndexAsync(user);
                    return new SignUpResponse
                    {
                        Message = Messages.SignUpSuccess
                    };
                }
                catch (Exception e)
                {
                    throw new ApplicationException(string.Format(Messages.SignUpFail, e.Message));
                }
            }
        }
    }
}