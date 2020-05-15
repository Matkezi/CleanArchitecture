﻿using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.EmailTemplateModels;
using SkipperAgency.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Bookings.Commands.GuestRequestBooking
{
    public class GuestRequestBookingCommand : IRequest
    {
        public string GuestEmail { get; set; }
        public int BookingId { get; set; }
        public string SkipperId { get; set; }

        public class Handler : IRequestHandler<GuestRequestBookingCommand>
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

            public async Task<Unit> Handle(GuestRequestBookingCommand request, CancellationToken cancellationToken)
            {
                var booking = await _context.Bookings
                    .Include(x => x.Charter)
                    .Include(x => x.Boat)
                    .FirstAsync(x => x.Id == request.BookingId, cancellationToken);

                booking.Status = BookingStatusEnum.SkipperRequested;
                booking.SkipperId = request.SkipperId;
                await _context.SaveChangesAsync(cancellationToken);

                var skipper = await _context.Skippers.FindAsync(booking.SkipperId);

                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/guest/booking/{booking.BookingUrl}/step=1";

                await _emailService.SendEmailWithTemplate(
                    new BookingRequested(
                        guestName: booking.GuestName,
                        toEmail: booking.GuestEmail,
                        skipperName: skipper.FullName,
                        bookingUrl: callbackUrl
                    ));

                string callbackUrl2 = $"{_configuration["AppSettings:AppServerUrl"]}/skipper/dashboard"; 
                _ = _emailService.SendEmailWithTemplate(
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