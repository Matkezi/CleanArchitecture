using AutoMapper;
using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Application.Metadata.Queries.GetCountries
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
