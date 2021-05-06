using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AioCore.Application.Commands.SecurityUserCommands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(ILogger logger)
        {
            RuleFor(command => command.Name).NotEmpty();

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}