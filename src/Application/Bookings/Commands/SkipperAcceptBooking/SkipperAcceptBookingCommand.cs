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
            private readonly IEmailService _emailService;
            private readonly IConfiguration _configuration;
            private readonly IApplicationDbContext _context;

            public Handler(IEmailService emailService, IConfiguration configuration, IApplicationDbContext context)
            {
                _emailService = emailService;
                _configuration = configuration;
                _context = context;
            }

            public async Task<Unit> Handle(SkipperAcceptBookingCommand request, CancellationToken cancellationToken)
            {
                var booking = await _context.Bookings.FindAsync(request.BookingId);
                booking.Status = BookingStatusEnum.SkipperAccepted;
                await _context.SaveChangesAsync(cancellationToken);

                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/guest/booking/{booking.BookingUrl}/step=1";

                var skipper = await _context.Skipper.FindAsync(booking.SkipperId);
                await _emailService.SendEmailWithTemplate(
                    new SkipperAccepted(
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
