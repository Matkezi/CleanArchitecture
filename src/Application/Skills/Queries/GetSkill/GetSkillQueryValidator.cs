using FluentValidation;

namespace SkipperAgency.Application.Skills.Queries.GetSkill
{
    public class GetSkillQueryVaidator : AbstractValidator<GetSkillQuery>
    {
        public GetSkillQueryVaidator()
        {
            RuleFor(x => x.SkillId).IsInEnum();
        }
    }
}
