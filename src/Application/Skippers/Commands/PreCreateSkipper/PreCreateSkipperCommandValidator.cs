using System.Linq;
using FluentValidation;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Skippers.Commands.PreCreateSkipper
{

    public class PreCreateSkipperCommandValidator : AbstractValidator<PreCreateSkipperCommand>
    {
        public PreCreateSkipperCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .Must(email => 
                    context.AppUsers.FirstOrDefault(x => x.Email == email) is null && 
                    context.SkipperPreRegistration.FirstOrDefault(x => x.Email == email) is null)
                .WithMessage("Email already taken");

        }
    }
}
