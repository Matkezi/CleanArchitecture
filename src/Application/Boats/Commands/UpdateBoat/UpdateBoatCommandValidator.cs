﻿using System;
using FluentValidation;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Boats.Commands.UpdateBoat
{
    public class UpdateBoatCommandValidator : AbstractValidator<UpdateBoatCommand>
    {
        public UpdateBoatCommandValidator(ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            RuleFor(x => x.Id).Must(bookingId =>
            {
                var boat = context.Boats.Find(bookingId);
                if (boat?.CharterId != currentUserService.UserId)
                {
                    throw new UnauthorizedAccessException($"Charter {currentUserService.UserId} not authorized to update boat {boat?.Id}.");
                }
                return true;
            });
        }
    }
}