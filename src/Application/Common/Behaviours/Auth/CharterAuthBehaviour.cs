using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Common.Behaviours.Auth
{
    public class CharterAuthBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : ICharterAuth
    {
        private readonly ICurrentUserService _currentUserService;

        public CharterAuthBehaviour(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var charterId = request.Id;
            if (charterId != userId)
            {
                throw new UnauthorizedAccessException($"Boat Charter {_currentUserService.UserId}");
            }

        }
    }
}
