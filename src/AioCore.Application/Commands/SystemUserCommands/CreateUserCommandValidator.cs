using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AioCore.Application.Commands.SystemUserCommands
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