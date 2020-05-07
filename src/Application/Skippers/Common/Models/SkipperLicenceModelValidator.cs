using CleanArchitecture.Application.Common.Models;
using FluentValidation;
using SkipperBooking.Web.Models;

namespace CleanArchitecture.Application.Skippers.Commands.UpdateSkipper
{
    public class SkipperLicenceModelValidator : AbstractValidator<SkipperLicenceModel>
    {
        public SkipperLicenceModelValidator()
        {
            RuleFor(x => x.LicenceType).IsInEnum();
        }
    }
}
