using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Emails
{
    public class BookingCreated : EmailMessage
    {
        public BookingCreated(string toEmail, string guestName, string charterName, string boatName, string bookedFrom, string bookedTo, string bookingURL) : base(toEmail)
        {
            GuestName = guestName;
            CharterName = charterName;
            BoatName = boatName;
            BookedFrom = bookedFrom;
            BookedTo = bookedTo;
            BookingURL = bookingURL;
        }

        public string GuestName { get; }
        public string CharterName { get; }
        public string BoatName { get; }
        public string BookedFrom { get; }
        public string BookedTo { get; }
        public string BookingURL { get; }
    }
}
