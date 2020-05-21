using SkipperAgency.Domain.EmailTemplateModels;

namespace SkipperAgency.Domain.EmailTemplates.BookingWaitingResponse
{
    class BookingWaitingResponseModel: EmailMessageAbstract
    {
        public BookingWaitingResponseModel(string toEmail, string skipperName, string guestName, string booking): base(toEmail)
        {
            SkipperName = skipperName;
            GuestName = guestName;
            Booking = booking;
        }

        public string SkipperName { get; set; }
        public string GuestName { get; set; }
        public string  Booking { get; set; }

    }
}
