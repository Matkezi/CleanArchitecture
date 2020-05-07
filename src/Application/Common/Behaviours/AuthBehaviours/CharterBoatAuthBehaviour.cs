using CleanArchitecture.Application.Bookings.Commands.SkipperAcceptBooking;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.TodoLists.Commands.DeleteTodoList;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Behaviours
{
    public class CharterBoatAuthBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : ICharterBoatAuth
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;

        public CharterBoatAuthBehaviour(ICurrentUserService currentUserService, ILogger logger, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _logger = logger;
            _context = context;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var entity = await _context.Bookings.FindAsync(request.BoatId);
            if (entity?.CharterId != userId)
            {
                // TODO: not sure if this logger is neccessary.
                _logger.LogError("");
                throw new UnauthorizedException($"Boat Charter", _currentUserService.UserId);
            }

        }
    }
}
