using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AioCore.Application.Commands.SystemTenantCommands
{
    public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidator(ILogger logger)
        {
            RuleFor(command => command.Name).NotEmpty();

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}