using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Common.Behaviours.Auth
{
    public class SkipperBookingAuthBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : ISkipperBookingAuth
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;

        public SkipperBookingAuthBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IApplicationDbContext context)
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
            if (entity?.SkipperId != userId)
            {
                throw new UnauthorizedException($"Booking Skipper", _currentUserService.UserId);
            }
        }
    }
}
