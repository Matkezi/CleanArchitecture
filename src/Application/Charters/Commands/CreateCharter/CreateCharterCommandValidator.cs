using FluentValidation;

namespace SkipperAgency.Application.Charters.Commands.CreateCharter
{

    public class CreateCharterCommandValidator : AbstractValidator<CreateCharterCommand>
    {
        public CreateCharterCommandValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");
            //.Must(userService.IsEmailUnique)
            //.WithMessage("Email already taken");

            RuleFor(x => x.GdprConsentAccepted).Must(x => x is true);
        }
    }
}
