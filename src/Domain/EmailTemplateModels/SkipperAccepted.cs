namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class SkipperAccepted : EmailMessage
    {
        public SkipperAccepted(string toEmail, string guestName, string skipperName, string bookingURL) : base(toEmail)
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
