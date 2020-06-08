using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Skippers.Queries.Availability.Common.Models;
using SkipperAgency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Skippers.Commands.Availability
{
    public class UpdateSkipperAvailabilityCommand : IRequest
    {
        public IEnumerable<AvailabilityDateRangeModel> Available { get; set; }
        public class Handler : IRequestHandler<UpdateSkipperAvailabilityCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<Unit> Handle(UpdateSkipperAvailabilityCommand request, CancellationToken cancellationToken)
            {
                var skipper = await _context.Skippers
                    .Include(x => x.Availability)
                    .FirstAsync(s => s.Id == _currentUserService.UserId, cancellationToken);

                skipper.Availability.ForEach(availability => _context.Availabilities.Remove(availability));
                skipper.Availability = request.Available
                    .Select(av => new Domain.Entities.Availability { AvailableFrom = av.From.AddHours(12), AvailableTo = av.To.AddHours(12) }).ToList();

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
