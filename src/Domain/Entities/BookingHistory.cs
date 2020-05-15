using SkipperAgency.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkipperAgency.Domain.Entities
{
    public class BookingHistory
    {
        public int Id { get; set; }
        public string SkipperId { get; set; }
        [ForeignKey(nameof(SkipperId))]
        public virtual Skipper Skipper { get; set; }
        public int BookingId { get; set; }
        [ForeignKey(nameof(BookingId))]
        public virtual Booking Booking { get; set; }
        public DateTime DateTime { get; set; }
        public BookingRejectedEnum BookingRejected { get; set; }
    }
}
