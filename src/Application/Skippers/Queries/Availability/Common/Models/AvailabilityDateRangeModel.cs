using AutoMapper;
using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;
using System;

namespace SkipperAgency.Application.Skippers.Queries.Availability.Common.Models
{
    public class AvailabilityDateRangeModel : IMapFrom<Domain.Entities.Availability>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Availability, AvailabilityDateRangeModel>().
                ForMember(dest => dest.From, opt => opt.MapFrom(src => src.AvailableFrom)).
                ForMember(dest => dest.To, opt => opt.MapFrom(src => src.AvailableTo));
        }
    }
}

