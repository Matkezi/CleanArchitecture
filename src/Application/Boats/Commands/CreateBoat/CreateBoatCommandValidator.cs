using FluentValidation;

namespace SkipperAgency.Application.Boats.Commands.CreateBoat
{
    public class CreateBoatCommandValidator : AbstractValidator<CreateBoatCommand>
    {
        public CreateBoatCommandValidator()
        {
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.MinimalRequiredLicense).IsInEnum();
        }
    }
}
