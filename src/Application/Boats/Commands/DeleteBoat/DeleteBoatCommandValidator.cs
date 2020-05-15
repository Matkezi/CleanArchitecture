using System;
using FluentValidation;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Boats.Commands.DeleteBoat
{
    public class DeleteBoatCommandValidator : AbstractValidator<DeleteBoatCommand>
    {
        public DeleteBoatCommandValidator(ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            RuleFor(x => x.Id).Must(bookingId =>
            {
                var boat = context.Boats.Find(bookingId);
                if (boat?.CharterId != currentUserService.UserId)
                {
                    throw new UnauthorizedAccessException($"Charter {currentUserService.UserId} not authorized to delete boat {boat?.Id}.");
                }
                return true;
            });
        }
    }
}