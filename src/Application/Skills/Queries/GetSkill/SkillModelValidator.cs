using FluentValidation;

namespace SkipperAgency.Application.Skills.Queries.GetSkill
{
    public class SkillModelValidator : AbstractValidator<SkillModel>
    {
        public SkillModelValidator()
        {
            RuleFor(x => x.SkillId).IsInEnum();
        }
    }
}
