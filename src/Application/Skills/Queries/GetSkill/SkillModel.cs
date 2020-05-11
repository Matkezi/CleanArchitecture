using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using SkipperBooking.Base.Enums;

namespace SkipperBooking.Web.Models
{
    public class SkillModel : IMapFrom<SkipperSkill>
    {
        public SkillsEnum SkillId { get; set; }
        public string SkipperId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }       
    }
}
