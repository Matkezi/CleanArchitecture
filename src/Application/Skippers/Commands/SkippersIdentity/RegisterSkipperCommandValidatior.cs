using FluentValidation;

namespace CleanArchitecture.Application.Skippers.Commands.SkippersIdentity
{

    public class RegisterSkipperCommandValidatior : AbstractValidator<RegisterSkipperCommand>
    {
        public RegisterSkipperCommandValidatior()
        {
            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");
            //.Must(userService.IsEmailUnique)
            //.WithMessage("Email already taken");

            RuleFor(x => x.GDPRConsentAccepted).Must(x => x is true);
        }
    }
}
