using FluentValidation;

namespace Rento.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8).WithMessage("Password must contain at least 8 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must have at least 1 uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must have at least 1 lowercase letter.")
                .Matches(@"\d").WithMessage("Password must have at least 1 number.")
                .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+\=]").WithMessage("Password must have at least 1 special character. (!@#$%^&*()-+=).");
        }
    }
}
