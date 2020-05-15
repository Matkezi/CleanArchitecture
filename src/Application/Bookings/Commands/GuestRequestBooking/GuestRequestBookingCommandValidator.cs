using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Bookings.Commands.GuestRequestBooking
{
    public class GuestRequestBookingCommandValidator : AbstractValidator<GuestRequestBookingCommand>
    {
        public GuestRequestBookingCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.GuestEmail).Must((request, guestEmail) =>
            {
                var booking = context.Bookings.Find(request.BookingId);
                if (booking?.GuestEmail != guestEmail)
                {
                    throw new UnauthorizedAccessException($"Booking not connected with {guestEmail}.");
                }
                return true;
            });
        }
    }
}
