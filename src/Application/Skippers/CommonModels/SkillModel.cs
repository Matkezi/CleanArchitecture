using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using SkipperBooking.Base.Enums;

namespace SkipperBooking.Web.Models
{
    public class SkillModel : IMapFrom<SkipperSkill>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Icon { get; set; }
        public string SkipperId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SkipperSkill, SkillModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.SkillId));

        }
    }
}
