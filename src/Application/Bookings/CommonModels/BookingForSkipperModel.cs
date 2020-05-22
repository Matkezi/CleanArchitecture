using SkipperAgency.Application.Boats.Queries.GetCharterBoats;
using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Application.Metadata.Queries.GetCountries;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkipperAgency.Application.Bookings.CommonModels
{
    public class BookingForSkipperModel: IMapFrom<Booking>
    {
        public int Id { get; set; }
        public int BoatId { get; set; }
        public BoatModel Boat { get; set; }
        public string SkipperId { get; set; }
        public string CharterId { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public BookingStatusEnum Status { get; set; }
        public SkipperActionEnum SkipperAction { get; set; }
        public string OnboardingLocation { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public CountryModel GuestNationality { get; set; }
        public string GuestMessege { get; set; }
        public int CrewSize { get; set; }
    }
}
