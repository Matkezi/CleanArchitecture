using CleanArchitecture.Application.Boats.Commands;
using FluentValidation;

namespace CleanArchitecture.Application.Skippers.Commands.SkippersIdentity
{
    public class CreateBoatCommandValidator : AbstractValidator<CreateBoatCommand>
    {
        public CreateBoatCommandValidator()
        {
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.MinimalRequiredLicence).IsInEnum();
        }
    }
}
