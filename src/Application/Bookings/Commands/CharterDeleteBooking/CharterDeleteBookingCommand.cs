﻿using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.TodoLists.Commands.DeleteTodoList
{
    public class CharterDeleteBookingCommand : IRequest
    {
        public string BookingId { get; set; }

        public class Handler : IRequestHandler<CharterDeleteBookingCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<Unit> Handle(CharterDeleteBookingCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Bookings
                    .FindAsync(request.BookingId);

                if (entity is null)
                {
                    throw new NotFoundException(nameof(CharterDeleteBookingCommand), request.BookingId);
                }

                _context.Bookings.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
