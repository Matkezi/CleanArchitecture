using System.Linq;
using FluentValidation;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Skippers.Commands.CreateSkipper
{
    public class CreateSkipperCommandValidator : AbstractValidator<CreateSkipperCommand>
    {
        public CreateSkipperCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .Must(email =>
                context.AppUser.FirstOrDefault(x => x.Email == email) is null &&
                context.SkipperPreRegistration.FirstOrDefault(x => x.Email == email) is null)
            .WithMessage("Email already taken");

            RuleFor(x => x.GdprConsentAccepted).Must(x => x is true);
        }
    }
}
