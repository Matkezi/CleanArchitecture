using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Common.Models
{
    public class CountryModel : IMapFrom<Country>
    {
        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string TwoLetterCode { get; set; }
        public string Label { get; set; }
        public string SkipperId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryModel>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.EnglishName));

        }
    }
}
