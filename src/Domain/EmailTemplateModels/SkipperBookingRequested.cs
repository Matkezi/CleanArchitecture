namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class SkipperBookingRequested : EmailMessage
    {
        public SkipperBookingRequested(string toEmail, string guestName, string skipperName, string charterName, string boatName, string bookings) : base(toEmail)
        {
            GuestName = guestName;
            SkipperName = skipperName;
            CharterName = charterName;
            BoatName = boatName;
            Bookings = bookings;
        }

        public string GuestName { get; }
        public string SkipperName { get; }
        public string CharterName { get; }
        public string BoatName { get; }
        public string Bookings { get; }

    }
}
