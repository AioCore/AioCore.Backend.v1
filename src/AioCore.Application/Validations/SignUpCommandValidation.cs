using AioCore.Application.Commands.IdentityCommands;
using AioCore.Shared;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Package.Localization;

namespace AioCore.Application.Validations
{
    public class SignUpCommandValidation : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidation(IStringLocalizer<Localization> localizer)
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Account).NotEmpty();
            RuleFor(t => t.Email).NotEmpty();
            RuleFor(t => t.Password).NotEmpty();
            RuleFor(t => t.ConfirmPassword).NotEmpty();
            RuleFor(t => t.TenantId).NotEmpty();
            RuleFor(t => t.ConfirmPassword).Equal(t => t.Password)
                .WithMessage(t => localizer[Message.SignUpMessagePasswordNotMatch]);
        }
    }
}
