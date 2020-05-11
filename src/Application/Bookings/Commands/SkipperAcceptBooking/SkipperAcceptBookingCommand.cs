using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.EmailTemplateModels;
using SkipperAgency.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Bookings.Commands.SkipperAcceptBooking
{
    public class SkipperAcceptBookingCommand : IRequest, ISkipperBookingAuth
    {
        public int BookingId { get; set; }

        public class Handler : IRequestHandler<SkipperAcceptBookingCommand>
        {
            private readonly IEmailService _emailer;
            private readonly IConfiguration _configuration;
            private readonly IApplicationDbContext _context;

            public Handler(IEmailService emailer, IConfiguration configuration, IApplicationDbContext context)
            {
                _emailer = emailer;
                _configuration = configuration;
                _context = context;
            }

            public async Task<Unit> Handle(SkipperAcceptBookingCommand request, CancellationToken cancellationToken)
            {
                var booking = await _context.Bookings.FindAsync(request.BookingId);
                booking.Status = BookingStatusEnum.SkipperAccepted;
                await _context.SaveChangesAsync(cancellationToken);

                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/guest/booking/{booking.BookingURL}/step=1";

                var skipper = await _context.Skipper.FindAsync(booking.SkipperId);
                await _emailer.SendEmailWithTemplate(
                    new SkipperAccepted(
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
