using AioCore.Application.Commands.IdentityCommands;
using AioCore.Shared.Constants;
using FluentValidation;

namespace AioCore.Application.Validations
{
    public class SignUpCommandValidation : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidation()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Account).NotEmpty();
            RuleFor(t => t.Email).NotEmpty();
            RuleFor(t => t.Password).NotEmpty();
            RuleFor(t => t.ConfirmPassword).NotEmpty();
            RuleFor(t => t.ConfirmPassword).Equal(t => t.Password)
                .WithMessage(t => Messages.SignUpPasswordNotMatch);
        }
    }
}