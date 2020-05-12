using System.IO;
using FluentValidation;
using SkipperAgency.Application.Common.ExtensionMethods;

namespace SkipperAgency.Application.Boats.Commands.CreateBoat
{
    public class CreateBoatCommandValidator : AbstractValidator<CreateBoatCommand>
    {
        public CreateBoatCommandValidator()
        {
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.MinimalRequiredLicense).IsInEnum();
            RuleFor(x => x.BoatPhoto)
                .Must(file => Path.HasExtension(file.NameWithExt))
                .ContainsValidBase64Data()
                .When(file => file != null);
        }
    }
}
