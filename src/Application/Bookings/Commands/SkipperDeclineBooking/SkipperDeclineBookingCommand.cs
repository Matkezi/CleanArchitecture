using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.EmailTemplateModels;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Application.Bookings.Commands.SkipperDeclineBooking
{
    public class SkipperDeclineBookingCommand : IRequest, ISkipperBookingAuth
    {
        public int BookingId { get; set; }

        public class Handler : IRequestHandler<SkipperDeclineBookingCommand>
        {
            private readonly IEmailService _emailer;
            private readonly IConfiguration _configuration;
            private readonly IApplicationDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IEmailService emailer, IConfiguration configuration, IApplicationDbContext context, IDateTime dateTime)
            {
                _emailer = emailer;
                _configuration = configuration;
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(SkipperDeclineBookingCommand request, CancellationToken cancellationToken)
            {

                var booking = await _context.Bookings.FindAsync(request.BookingId);

                BookingHistory bookingHistory = new BookingHistory
                {
                    Booking = booking,
                    BookingId = booking.Id,
                    Skipper = booking.Skipper,
                    SkipperId = booking.Skipper.Id,
                    dateTime = _dateTime.Now
                };
                _context.BookingHistories.Add(bookingHistory);

                booking.Status = BookingStatusEnum.SkipperRequestPending;
                booking.Skipper = null;
                booking.SkipperId = null;
                await _context.SaveChangesAsync(cancellationToken);

                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/guest/booking/{booking.BookingURL}/step=1";

                var skipper = await _context.Skipper.FindAsync(booking.SkipperId);
                await _emailer.SendEmailWithTemplate(
                    new SkipperDeclined(
                        guestName: booking.GuestName,
                        toEmail: booking.GuestEmail,
                        skipperName: skipper.FullName,
                        bookingURL: callbackUrl
                    ));

                return Unit.Value;
            }
        }
    }
}
