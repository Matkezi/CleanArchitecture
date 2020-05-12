using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Common.Behaviours.Auth
{
    public class SkipperBookingAuthBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : ISkipperBookingAuth
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;

        public SkipperBookingAuthBehaviour( ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var booking = await _context.Bookings.FindAsync(request.Id);
            if (booking?.SkipperId != userId)
            {
                throw new UnauthorizedAccessException($"Skipper {_currentUserService.UserId} is not authorized for booking {booking?.Id}.");
            }
        }
    }
}
