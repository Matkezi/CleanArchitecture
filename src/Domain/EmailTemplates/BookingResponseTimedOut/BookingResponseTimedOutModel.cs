using SkipperAgency.Domain.EmailTemplateModels;

namespace SkipperAgency.Domain.EmailTemplates.BookingResponseTimedOut
{
    class BookingResponseTimedOutModel: EmailMessageAbstract
    {
        public BookingResponseTimedOutModel(string toEmail, string skipperName, string bookings) : base(toEmail)
        {
            SkipperName = skipperName;
            Bookings = bookings;
        }

        public string SkipperName { get; }
        public string Bookings { get; }
    }
}