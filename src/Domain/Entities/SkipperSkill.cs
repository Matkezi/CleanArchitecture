using System;
using System.Collections.Generic;
using System.Text;
using SkipperBooking.Base.Enums;

namespace CleanArchitecture.Infrastructure.Persistence.Entities
{
    public class SkipperSkill
    {

        public string SkipperId { get; set; }
        public Skipper Skipper { get; set; }
        public SkillsEnum SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
