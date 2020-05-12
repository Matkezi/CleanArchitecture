using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Common.Behaviours.Auth
{
    public class CharterBookingsAuthBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : ICharterBookingAuth
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;

        public CharterBookingsAuthBehaviour(ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var entity = await _context.Bookings.FindAsync(request.Id);
            if (entity?.CharterId != userId)
            {
                throw new UnauthorizedAccessException($"Boat Charter {_currentUserService.UserId}");
            }
        }
    }
}
