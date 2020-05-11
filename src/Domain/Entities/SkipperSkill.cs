using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Domain.Entities
{
    public class SkipperSkill
    {

        public string SkipperId { get; set; }
        public Skipper Skipper { get; set; }
        public SkillsEnum SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
