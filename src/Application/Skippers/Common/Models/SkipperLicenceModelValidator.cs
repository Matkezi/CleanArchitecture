using FluentValidation;

namespace SkipperAgency.Application.Skippers.Common.Models
{
    public class SkipperLicenceModelValidator : AbstractValidator<SkipperLicenceModel>
    {
        public SkipperLicenceModelValidator()
        {
            RuleFor(x => x.LicenceType).IsInEnum();
        }
    }
}
