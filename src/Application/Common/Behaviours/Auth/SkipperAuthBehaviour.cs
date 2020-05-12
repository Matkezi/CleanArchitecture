using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Common.Behaviours.Auth
{
    public class SkipperAuthBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : ISkipperAuth
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public SkipperAuthBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var skipperId = request.SkipperId;
            if (skipperId != userId)
            {
                throw new UnauthorizedException($"Skipper", _currentUserService.UserId);
            }
        }
    }
}
