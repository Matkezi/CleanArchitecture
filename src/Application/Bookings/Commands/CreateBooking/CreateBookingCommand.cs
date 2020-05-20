using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Boats.Queries.GetCharterBoats;
using SkipperAgency.Application.Common.Exceptions;
using SkipperAgency.Application.Common.ExtensionMethods;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Metadata.Queries.GetCountries;
using SkipperAgency.Domain.EmailTemplateModels;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Application.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest
    {
        public int Id { get; set; }
        public BoatModel Boat { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public string OnboardingLocation { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public CountryModel GuestNationality { get; set; }

        public class Handler : IRequestHandler<CreateBookingCommand>
        {
            private readonly IEmailService _emailService;
            private readonly IConfiguration _configuration;
            private readonly ICurrentUserService _currentUserService;
            private readonly IApplicationDbContext _context;

            public Handler(IEmailService emailService, IConfiguration configuration, ICurrentUserService currentUserService, IApplicationDbContext context)
            {
                _emailService = emailService;
                _configuration = configuration;
                _currentUserService = currentUserService;
                _context = context;
            }

            public async Task<Unit> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
            {
                var charter = await _context.Charters.FindAsync(_currentUserService.UserId);
                if (charter is null)
                {
                    throw new NotFoundException(nameof(charter), _currentUserService.UserId);
                }

                var booking = new Booking
                {
                    CharterId = charter.Id,
                    BoatId = request.Boat.Id,
                    BookedFrom = request.BookedFrom,
                    BookedTo = request.BookedTo,
                    GuestName = request.GuestName,
                    GuestEmail = request.GuestEmail,
                    GuestNationalityId = request.GuestNationality.Id,
                    OnBoardingLocation = request.OnboardingLocation,
                    Status = BookingStatusEnum.SkipperRequestPending,
                    BookingUrl = RandomUrl.GetRandomUrl(),
                };

                await _context.Bookings.AddAsync(booking, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                
                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/guest/booking/{booking.BookingUrl}/step=1";
                _ = _emailService.SendEmailWithTemplate(
                    new BookingCreated(
                        guestName: request.GuestName,
                        toEmail: booking.GuestEmail,
                        charterName: charter.CharterName,
                        boatName: "",
                        bookedFrom: booking.BookedFrom.ToShortDateString(),
                        bookedTo: booking.BookedTo.ToShortDateString(),
                        bookingUrl: callbackUrl
                    ));

                return Unit.Value;
            }
        }
    }
}
