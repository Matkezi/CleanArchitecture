using SkipperAgency.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkipperAgency.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int BoatId { get; set; }
        [ForeignKey(nameof(BoatId))]
        public virtual Boat Boat { get; set; }
        public string SkipperId { get; set; }
        [ForeignKey(nameof(SkipperId))]
        public virtual Skipper Skipper { get; set; }
        public string CharterId { get; set; }
        [ForeignKey(nameof(CharterId))]
        public virtual Charter Charter { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public BookingStatusEnum Status { get; set; }
        public string BookingUrl { get; set; }
        public List<BookingHistory> BookingHistories { get; set; }
        public string OnBoardingLocation { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public int? GuestNationalityId { get; set; }
        [ForeignKey(nameof(GuestNationalityId))]
        public Country GuestNationality { get; set; }
        public string GuestMessage { get; set; }
        public bool GuestTos { get; set; }
        public int CrewSize { get; set; }
        public DateTime SkipperRequestTime { get; set; }
    }
}
