using FluentValidation;
using SkipperBooking.Web.Models;

namespace CleanArchitecture.Application.Skippers.Commands.UpdateSkipper
{
    public class SkillModelValidator : AbstractValidator<SkillModel>
    {
        public SkillModelValidator()
        {
            RuleFor(x => x.SkillId).IsInEnum();
        }
    }
}
