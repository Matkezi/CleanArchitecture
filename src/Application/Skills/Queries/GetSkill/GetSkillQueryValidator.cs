using FluentValidation;

namespace SkipperAgency.Application.Skills.Queries.GetSkill
{
    public class GetSkillQueryValidator : AbstractValidator<GetSkillQuery>
    {
        public GetSkillQueryValidator()
        {
            RuleFor(x => x.SkillId).IsInEnum();
        }
    }
}
