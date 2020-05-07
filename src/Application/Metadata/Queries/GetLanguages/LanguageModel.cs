using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Infrastructure.Persistence.Entities;

namespace CleanArchitecture.Application.Skippers.Models1
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
