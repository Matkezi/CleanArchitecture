namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class SkipperAccepted : EmailMessageAbstract
    {
        public SkipperAccepted(string toEmail, string guestName, string skipperName, string bookingUrl) : base(toEmail)
        {
            GuestName = guestName;
            SkipperName = skipperName;
            BookingUrl = bookingUrl;
        }

        public string GuestName { get; }
        public string SkipperName { get; }
        public string BookingUrl { get; }

    }
}
