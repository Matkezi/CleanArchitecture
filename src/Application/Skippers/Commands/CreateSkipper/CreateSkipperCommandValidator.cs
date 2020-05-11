using FluentValidation;

namespace SkipperAgency.Application.Skippers.Commands.CreateSkipper
{
    public class CreateSkipperCommandValidator : AbstractValidator<CreateSkipperCommand>
    {
        public CreateSkipperCommandValidator()
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
