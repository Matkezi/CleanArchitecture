using AutoMapper;
using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;
using System;

namespace SkipperAgency.Application.Skippers.Queries.Availability.Common.Models
{
    public class BookingDateRangeModel : IMapFrom<Booking>
    {        
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Booking, BookingDateRangeModel>().
                ForMember(dest => dest.From, opt => opt.MapFrom(src => src.BookedFrom)).
                ForMember(dest => dest.To, opt => opt.MapFrom(src => src.BookedTo));
        }
    }
}
