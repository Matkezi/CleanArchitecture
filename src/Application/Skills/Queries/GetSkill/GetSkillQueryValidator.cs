using CleanArchitecture.Application.Skippers.Queries.GetSkipper;
using FluentValidation;
using SkipperBooking.Web.Models;

namespace CleanArchitecture.Application.Skippers.Commands.UpdateSkipper
{
    public class GetSkillQueryVaidator : AbstractValidator<GetSkillQuery>
    {
        public GetSkillQueryVaidator()
        {
            RuleFor(x => x.SkillId).IsInEnum();
        }
    }
}
