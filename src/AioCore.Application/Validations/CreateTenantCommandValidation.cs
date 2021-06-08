using AioCore.Application.Commands.SystemTenantCommands;
using FluentValidation;

namespace AioCore.Application.Validations
{
    public class CreateTenantCommandValidation : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidation()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Database).NotEmpty();
            RuleFor(t => t.Password).NotEmpty();
            RuleFor(t => t.Schema).NotEmpty();
            RuleFor(t => t.Server).NotEmpty();
            RuleFor(t => t.User).NotEmpty();
            RuleFor(t => t.DatabaseType).NotEmpty();
        }
    }
}
