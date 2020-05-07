using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Infrastructure.Persistence.Entities;

namespace CleanArchitecture.Application.Skippers.Models
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
