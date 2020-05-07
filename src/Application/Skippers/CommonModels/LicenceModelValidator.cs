using CleanArchitecture.Application.Common.Models;
using FluentValidation;
using SkipperBooking.Web.Models;

namespace CleanArchitecture.Application.Skippers.Commands.UpdateSkipper
{
    public class LicenceModelValidator : AbstractValidator<LicenceModel>
    {
        public LicenceModelValidator()
        {
            RuleFor(x => x.LicenceType).IsInEnum();
        }
    }
}
