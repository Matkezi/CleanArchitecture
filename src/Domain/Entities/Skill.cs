using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Domain.Entities
{
    public class Skill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public SkillsEnum Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public List<SkipperSkill> Skippers { get; set; }
    }
}
