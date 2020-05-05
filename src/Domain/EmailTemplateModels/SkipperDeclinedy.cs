using CleanArchitecture.Domain.Emails;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.EmailTemplateModels
{
    public class SkipperDeclined : EmailMessage
    {
        public SkipperDeclined(string toEmail, string guestName, string skipperName, string bookingURL) : base(toEmail)
        {
            GuestName = guestName;
            SkipperName = skipperName;
            BookingURL = bookingURL;
        }

        public string GuestName { get; }
        public string SkipperName { get; }
        public string BookingURL { get; }

    }
}
