using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
    {
        public DeleteBookingCommandValidator(ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            RuleFor(x => x.Id).Must(bookingId =>
            {
                var booking =  context.Bookings.Find(bookingId);
                if (booking?.CharterId != currentUserService.UserId)
                {
                    throw new UnauthorizedAccessException($"Charter {currentUserService.UserId} not authorized for booking {booking?.Id}.");
                }
                return true;
            });
        }
    }

}