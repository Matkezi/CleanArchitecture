using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SkipperAgency.Domain.Enums;

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
        public string BookingURL { get; set; }
        public List<BookingHistory> BookingHistories { get; set; }
        public string OnboardingLocation { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public int? GuestNationalityId { get; set; }
        [ForeignKey(nameof(GuestNationalityId))]
        public Country GuestNationality { get; set; }
        public string GuestMessege { get; set; }
        public bool GuestTOS { get; set; }
        public int CrewSize { get; set; }
        public DateTime SkipperRequestTime { get; set; }
    }
}
