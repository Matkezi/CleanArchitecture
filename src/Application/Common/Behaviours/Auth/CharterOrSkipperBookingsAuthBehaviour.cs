using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Common.Behaviours.Auth
{
    public class CharterOrSkipperBookingsAuthBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : ICharterOrSkipperBookingAuth
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;

        public CharterOrSkipperBookingsAuthBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var bookingId = request.BookingId;
            var entity = await _context.Bookings.FindAsync(bookingId);
            if (entity?.CharterId != userId || entity?.SkipperId != userId)
            {
                throw new UnauthorizedException($"Booking Charter", _currentUserService.UserId);
            }
        }
    }
}
