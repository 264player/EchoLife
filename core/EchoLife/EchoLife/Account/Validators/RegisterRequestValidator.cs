using EchoLife.Account.Dtos;
using FluentValidation;

namespace EchoLife.Account.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(r => r.Username).NotEmpty().Length(3, 64);
        RuleFor(r => r.Password).NotEmpty().Length(6, 64);
        RuleFor(r => r.EnsurePassword).Equal(r => r.Password);
    }
}
