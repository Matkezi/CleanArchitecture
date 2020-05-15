using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Application.Skills.Queries.GetSkill
{
    public class SkillModel : IMapFrom<SkipperSkill>
    {
        public SkillsEnum SkillId { get; set; }
        public string SkipperId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
