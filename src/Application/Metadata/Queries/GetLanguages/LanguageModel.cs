using AutoMapper;
using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Application.Metadata.Queries.GetLanguages
{
    public class LanguageModel : IMapFrom<Language>
    {
        public int LanguageId { get; set; }
        public string Label { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Language, LanguageModel>()
                .ForMember(dest => dest.Label,
                            opt => opt.MapFrom(src => src.EnglishName));
        }
    }
}
