using AioCore.Application.Responses;
using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.IdentityCommands
{
    public class SignUpCommand : IRequest<ApiResponseModel<bool>>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        internal class Handler : IRequestHandler<SignUpCommand, ApiResponseModel<bool>>
        {
            private readonly ISecurityUserRepository _securityUserRepository;

            public Handler(ISecurityUserRepository securityUserRepository)
            {
                _securityUserRepository = securityUserRepository ?? throw new ArgumentNullException(nameof(securityUserRepository));
            }

            public async Task<ApiResponseModel<bool>> Handle(SignUpCommand request, CancellationToken cancellationToken)
            {
                if (!request.Password.Equals(request.ConfirmPassword))
                    return new ApiResponseModel<bool> { ResponseType = HttpStatusCode.BadRequest, Message = "Password does not match" };
                _securityUserRepository.Add(new SecurityUser(request.Name, request.Email, request.Password));
                await _securityUserRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return new ApiResponseModel<bool> { Message = "Sign up successfully", ResponseType = HttpStatusCode.Created };
            }
        }
    }
}