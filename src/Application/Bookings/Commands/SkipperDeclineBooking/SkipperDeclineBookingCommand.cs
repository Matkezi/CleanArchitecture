using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.EmailTemplateModels;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Bookings.Commands.SkipperDeclineBooking
{
    public class SkipperDeclineBookingCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<SkipperDeclineBookingCommand>
        {
            private readonly IEmailService _emailService;
            private readonly IConfiguration _configuration;
            private readonly IApplicationDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IEmailService emailService, IConfiguration configuration, IApplicationDbContext context, IDateTime dateTime)
            {
                _emailService = emailService;
                _configuration = configuration;
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(SkipperDeclineBookingCommand request, CancellationToken cancellationToken)
            {

                var booking = await _context.Bookings.FindAsync(request.Id);

                var bookingHistory = new BookingHistory
                {
                    Booking = booking,
                    BookingId = booking.Id,
                    Skipper = booking.Skipper,
                    SkipperId = booking.Skipper.Id,
                    DateTime = _dateTime.Now
                };
                await _context.BookingHistories.AddAsync(bookingHistory, cancellationToken);

                booking.Status = BookingStatusEnum.SkipperRequestPending;
                booking.Skipper = null;
                booking.SkipperId = null;
                await _context.SaveChangesAsync(cancellationToken);

                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/guest/booking/{booking.BookingUrl}/step=1";

                var skipper = await _context.Skippers.FindAsync(booking.SkipperId);
                await _emailService.SendEmailWithTemplate(
                    new SkipperDeclined(
                        guestName: booking.GuestName,
                        toEmail: booking.GuestEmail,
                        skipperName: skipper.FullName,
                        bookingUrl: callbackUrl
                    ));

                return Unit.Value;
            }
        }
    }
}
