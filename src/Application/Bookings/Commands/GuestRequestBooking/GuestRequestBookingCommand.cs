using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Emails;
using CleanArchitecture.Domain.EmailTemplateModels;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SkipperBooking.Base.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CleanArchitecture.Application.Skippers.Commands.SkippersIdentity
{
    public class GuestRequestBookingCommand : IRequest
    {
        public string GuestEmail { get; set; }
        public int BookingId { get; set; }
        public string SkipperId { get; set; }

        public class Handler : IRequestHandler<GuestRequestBookingCommand>
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

            public async Task<Unit> Handle(GuestRequestBookingCommand request, CancellationToken cancellationToken)
            {

                Booking booking = await _context.Bookings.Include(x => x.Charter).Include(x => x.Boat).FirstAsync(x => x.Id == request.BookingId);
                
                if (booking.GuestEmail != request.GuestEmail)
                {
                    throw new UnauthorizedException("Booking", request.GuestEmail);
                }

                booking.Status = BookingStatusEnum.SkipperRequested;
                booking.SkipperId = request.SkipperId;
                await _context.SaveChangesAsync(cancellationToken);

                var skipper = await _context.Skipper.FindAsync(booking.SkipperId);

                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/guest/booking/{booking.BookingURL}/step=1";
                
                await _emailer.SendEmailWithTemplate(
                    new BookingRequested(
                        guestName: booking.GuestName,
                        toEmail: booking.GuestEmail,
                        skipperName: skipper.FullName,
                        bookingURL: callbackUrl
                    ));


                string callbackUrl2 = $"{_configuration["AppSettings:AppServerUrl"]}/skipper/dashboard";
                await _emailer.SendEmailWithTemplate(
                    new SkipperBookingRequested(
                        guestName: booking.GuestName,
                        toEmail: skipper.Email,
                        skipperName: skipper.FullName,
                        charterName: booking.Charter.CharterName,
                        boatName: booking.Boat.Name,
                        bookings: callbackUrl2
                    ));

                return Unit.Value;
            }
        }
    }
}
