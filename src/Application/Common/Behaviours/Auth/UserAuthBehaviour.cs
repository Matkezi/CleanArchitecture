using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Common.Behaviours.Auth
{
    public class UserAuthBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : IUserAuth
    {
        private readonly ICurrentUserService _currentUserService;

        public UserAuthBehaviour(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId != request.Id)
            {
                throw new UnauthorizedAccessException($"Currently logged in user is not authorized for this action.");
            }

        }
    }
}
