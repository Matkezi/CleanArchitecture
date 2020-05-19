using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;
using AutoMapper;

namespace SkipperAgency.Application.Skills.Queries.GetSkill
{
    public class SkillModel : IMapFrom<SkipperSkill>
    {
        public SkillsEnum SkillId { get; set; }
        public string SkipperId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SkipperSkill, SkillModel>().
                ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Skill.Icon)).
                ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Skill.Name)).
                ForMember(dest => dest.SkillId, opt => opt.MapFrom(src => src.Skill.Id)).
                ForMember(dest => dest.SkipperId, opt => opt.MapFrom(src => src.SkipperId));

            profile.CreateMap<Skill, SkillModel>().ReverseMap();
                
        }
    }
}
