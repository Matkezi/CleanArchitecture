using SkipperAgency.Application.Boats.Queries.GetCharterBoats;
using SkipperAgency.Application.Charters.Queries.GetCharter;
using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Application.Metadata.Queries.GetCountries;
using SkipperAgency.Application.Skippers.Common.Models;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;
using System;
using System.Collections.Generic;

namespace SkipperAgency.Application.Bookings.CommonModels
{
    public class BookingModel: IMapFrom<Booking>
    {
        public int Id { get; set; }
        public int BoatId { get; set; }
        public BoatModel Boat { get; set; }
        public string SkipperId { get; set; }
        public SkipperSimpleModel Skipper { get; set; }
        public string CharterId { get; set; }
        public CharterModel Charter { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public BookingStatusEnum Status { get; set; }
        public string BookingURL { get; set; }
        public List<BookingHistoryModel> BookingHistories { get; set; }
        public SkipperActionEnum SkipperAction { get; set; }
        public string OnboardingLocation { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public bool GuestTOS { get; set; }
        public CountryModel GuestNationality { get; set; }
        public string GuestMessege { get; set; }
        public int CrewSize { get; set; }
        public DateTime SkipperRequestTime { get; set; }
    }

}
