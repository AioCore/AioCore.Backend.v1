using AioCore.Application.Commands.SystemTenantCommands;
using FluentValidation;

namespace AioCore.Application.Validations
{
    public class CreateTenantCommandValidation : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidation()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Database).NotNull();
            RuleFor(t => t.Elasticsearch).NotNull();
        }
    }
}
