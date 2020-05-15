namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class BookingCreated : EmailMessageAbstract
    {
        public BookingCreated(string toEmail, string guestName, string charterName, string boatName, string bookedFrom, string bookedTo, string bookingUrl) : base(toEmail)
        {
            GuestName = guestName;
            CharterName = charterName;
            BoatName = boatName;
            BookedFrom = bookedFrom;
            BookedTo = bookedTo;
            BookingUrl = bookingUrl;
        }

        public string GuestName { get; }
        public string CharterName { get; }
        public string BoatName { get; }
        public string BookedFrom { get; }
        public string BookedTo { get; }
        public string BookingUrl { get; }
    }
}
