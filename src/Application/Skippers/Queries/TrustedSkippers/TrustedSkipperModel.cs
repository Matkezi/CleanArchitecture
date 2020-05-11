using AutoMapper;
using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Application.Skippers.Common.Models;
using SkipperAgency.Domain.Entities;
using System.Collections.Generic;

namespace SkipperAgency.Application.Skippers.Queries.TrustedSkippers
{
    public class TrustedSkipperModel : IMapFrom<Skipper>
    {
        public string Id { get; set; }
        public string ImageURL { get; set; }
        public string FirstName { get; set; }
        public string YearOfFirstLicence { get; set; }
        public IEnumerable<SkipperLanguageModel> ListOfLanguages { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Skipper, TrustedSkipperModel>()
                .ForMember(dest => dest.ImageURL,
                            opt => opt.MapFrom(src => src.UserPhotoUrl));
        }
    }
}
