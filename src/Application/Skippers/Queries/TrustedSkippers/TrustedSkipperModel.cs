using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Skippers.Models
{
    public class TrustedSkipperModel : IMapFrom<Skipper>
    {
        public string Id { get; set; }
        public string ImageURL { get; set; }
        public string FirstName { get; set; }
        public string YearOfFirstLicence { get; set; }
        public IEnumerable<LanguageModel> ListOfLanguages { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Skipper, TrustedSkipperModel>()
                .ForMember(dest => dest.ImageURL,
                            opt => opt.MapFrom(src => src.UserPhotoUrl));
        }
    }
}
