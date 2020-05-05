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
    public class BookingsAuthorizationBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;
        private readonly IApplicationDbContext _context;

        public BookingsAuthorizationBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IIdentityService identityService, IApplicationDbContext context)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _identityService = identityService;
            _context = context;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {

            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await _identityService.GetUserNameAsync(userId);
            }

            if (requestName.EndsWith("BookingCommand"))
            {
                var bookingId = request.GetType().GetProperty("BookingId").GetValue(request, null) as int?;
                if (bookingId != null)
                {
                    var entity = await _context.Bookings.FindAsync(bookingId);
                    if (requestName.StartsWith("Charter")
                        && entity.CharterId != _currentUserService.UserId)
                    {
                        // TODO: not sure if this logger is neccessary.
                        _logger.LogError("");
                        throw new UnauthorizedException($"Booking", _currentUserService.UserId);
                    }
                    else if (requestName.StartsWith("Skipper")
                        && entity.SkipperId != _currentUserService.UserId)
                    {
                        // TODO: not sure if this logger is neccessary.
                        _logger.LogError("");
                        throw new UnauthorizedException($"Booking", _currentUserService.UserId);
                    }
                }
            }

            _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }
    }
}
