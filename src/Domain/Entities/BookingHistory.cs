using SkipperBooking.Base.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CleanArchitecture.Infrastructure.Persistence.Entities
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
        public DateTime dateTime { get; set; }
        public BookingRejectedEnum bookingRejected { get; set; }
    }
}
