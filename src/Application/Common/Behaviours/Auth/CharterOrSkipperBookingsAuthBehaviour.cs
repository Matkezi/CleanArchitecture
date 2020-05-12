using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Common.Behaviours.Auth
{
    public class CharterOrSkipperBookingsAuthBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : ICharterOrSkipperBookingAuth
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;

        public CharterOrSkipperBookingsAuthBehaviour(ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var bookingId = request.BookingId;
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking?.CharterId != userId || booking?.SkipperId != userId)
            {
                throw new UnauthorizedAccessException($"User {_currentUserService.UserId} is not authorized for booking {booking?.Id}.");
            }
        }
    }
}
