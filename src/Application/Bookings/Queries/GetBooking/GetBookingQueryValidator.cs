using System;
using FluentValidation;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Bookings.Queries.GetBooking
{
    public class GetBookingQueryValidator : AbstractValidator<GetBookingQuery>
    {
        public GetBookingQueryValidator(ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            RuleFor(x => x.Id).Must(bookingId =>
            {
                var booking = context.Bookings.Find(bookingId);
                if (booking?.CharterId != currentUserService.UserId || booking?.SkipperId != currentUserService.UserId)
                {
                    throw new UnauthorizedAccessException($"User {currentUserService.UserId} is not authorized to get booking {booking?.Id}.");
                }
                return true;
            });
        }
    }
}
