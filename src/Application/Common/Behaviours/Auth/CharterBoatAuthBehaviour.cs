﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Common.Behaviours.Auth
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
            var boat = await _context.Boats.FindAsync(request.Id);
            if (boat?.CharterId != userId)
            {
                throw new UnauthorizedAccessException($"Charter {_currentUserService.UserId} not authorized for boat {boat?.Id}.");
            }

        }
    }
}
