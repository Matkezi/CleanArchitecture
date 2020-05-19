using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Application.Skippers.Common.Models;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkipperAgency.Application.Bookings.CommonModels
{
    public class BookingHistoryModel: IMapFrom<BookingHistory>
    {
        public int Id { get; set; }
        public string SkipperId { get; set; }
        public virtual SkipperSimpleModel Skipper { get; set; }
        public virtual BookingModel Booking { get; set; }
        public DateTime dateTime { get; set; }
        public BookingRejectedEnum bookingRejected { get; set; }
    }
}
