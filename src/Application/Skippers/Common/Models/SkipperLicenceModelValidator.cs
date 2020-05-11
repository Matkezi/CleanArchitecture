using FluentValidation;

namespace SkipperAgency.Application.Skippers.Common.Models
{
    public class SkipperLicenseModelValidator : AbstractValidator<SkipperLicenseModel>
    {
        public SkipperLicenseModelValidator()
        {
            RuleFor(x => x.LicenseType).IsInEnum();
        }
    }
}
