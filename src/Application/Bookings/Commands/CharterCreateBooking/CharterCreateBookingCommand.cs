using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Emails;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperBooking.Base.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CleanArchitecture.Application.Skippers.Commands.SkippersIdentity
{
    public class CharterCreateBookingCommand : IRequest
    {
        public int BoatId { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public string OnboardingLocation { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }

        public class Handler : IRequestHandler<CharterCreateBookingCommand>
        {
            private readonly IEmailService _emailer;
            private readonly IConfiguration _configuration;
            private readonly ICurrentUserService _currentUserService;
            private readonly IApplicationDbContext _context;

            public Handler(IEmailService emailer, IConfiguration configuration, ICurrentUserService currentUserService, IApplicationDbContext context)
            {
                _emailer = emailer;
                _configuration = configuration;
                _currentUserService = currentUserService;
                _context = context;
            }

            public async Task<Unit> Handle(CharterCreateBookingCommand request, CancellationToken cancellationToken)
            {
                var charter = await _context.Charter.FindAsync(_currentUserService.UserId);
                if (charter is null)
                {
                    // TODO: Exception? Because there must be a charter in order to create a booking.
                }

                var booking = new Booking
                {
                    CharterId = charter.Id,
                    BoatId = request.BoatId,
                    BookedFrom = request.BookedFrom,
                    BookedTo = request.BookedTo,
                    GuestName = request.GuestName,
                    GuestEmail = request.GuestEmail,
                    OnboardingLocation = request.OnboardingLocation,
                    Status = BookingStatusEnum.SkipperRequestPending,
                    BookingURL = RandomUrl.GetRandomUrl(),
                };
                
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync(cancellationToken);

                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/guest/booking/{booking.BookingURL}/step=1";
                await _emailer.SendEmailWithTemplate(
                    new BookingCreated(
                        guestName: request.GuestName,
                        toEmail: booking.GuestEmail,
                        charterName: charter.CharterName,
                        boatName: "",
                        bookedFrom: booking.BookedFrom.ToShortDateString(),
                        bookedTo: booking.BookedTo.ToShortDateString(),
                        bookingURL: callbackUrl
                    ));

                return Unit.Value;
            }
        }
    }
}
