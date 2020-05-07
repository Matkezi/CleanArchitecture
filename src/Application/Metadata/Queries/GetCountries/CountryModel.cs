using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Infrastructure.Persistence.Entities;

namespace CleanArchitecture.Application.Common.Models
{
    public class CountryModel : IMapFrom<Country>
    {
        public int Id { get; set; }
        public string Label { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryModel>()
                .ForMember(dest => dest.Label,
                            opt => opt.MapFrom(src => src.EnglishName));
        }

    }
}
