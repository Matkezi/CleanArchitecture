namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class BookingRequestedModel : EmailMessageAbstract
    {
        public BookingRequestedModel(string toEmail, string guestName, string skipperName, string bookingUrl) : base(toEmail)
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
