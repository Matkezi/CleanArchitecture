using System;
using FluentValidation;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Bookings.Commands.SkipperAcceptBooking
{
    public class SkipperAcceptBookingCommandValidator : AbstractValidator<SkipperAcceptBookingCommand>
    {
        public SkipperAcceptBookingCommandValidator(ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            //RuleFor(x => x.Id).Must(bookingId =>
            //{
            //    var booking = context.Bookings.Find(bookingId);
            //    if (booking?.SkipperId != currentUserService.UserId)
            //    {
            //        throw new UnauthorizedAccessException($"Skipper {currentUserService.UserId} not to accept booking {booking?.Id}.");
            //    }
            //    return true;
            //});
        }
    }
}
