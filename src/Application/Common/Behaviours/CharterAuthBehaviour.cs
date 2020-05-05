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
    public class CharterAuthBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : ICharterAuth
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public CharterAuthBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var charterId = request.CharterId;
            if (charterId != userId)
            {
                // TODO: not sure if this logger is neccessary.
                _logger.LogError("");
                throw new UnauthorizedException($"Skipper", _currentUserService.UserId);
            }

        }
    }
}
