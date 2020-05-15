using AutoMapper;
using SkipperAgency.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SkipperAgency.Application.Skippers.Queries.Availability.Common.Models
{
    public class AvailabilityModel
    {
        public IEnumerable<(DateTime BookedFrom, DateTime BookedTo)> Booked { get; set; }
        public IEnumerable<(DateTime AvailableFrom, DateTime AvailableTo)> Available { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Skipper, AvailabilityModel>()
                .ForMember(dest => dest.Available, opt => opt.MapFrom(src => src.Availability))
                .ForMember(dest => dest.Booked, opt => opt.MapFrom(src => src.Bookings));
        }
    }
}
