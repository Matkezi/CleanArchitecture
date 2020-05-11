using AutoMapper;
using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Application.Skippers.Common.Models
{
    public class SkipperLanguageModel : IMapFrom<SkipperLanguage>
    {
        public int LanguageId { get; set; }
        public string SkipperId { get; set; }
        public string Label { get; set; }
        public int LevelOfKnowledge { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SkipperLanguage, SkipperLanguageModel>()
                .ForMember(dest => dest.Label,
                            opt => opt.MapFrom(src => src.Language.EnglishName));
        }
    }
}
