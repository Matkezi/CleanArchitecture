using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Skippers.Commands.TrustedSkippers
{
    public class UpdateTrustedSkippersCommand : IRequest
    {
        public IEnumerable<string> Ids { get; set; }

        public class Handler : IRequestHandler<UpdateTrustedSkippersCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<Unit> Handle(UpdateTrustedSkippersCommand request, CancellationToken cancellationToken)
            {
                var charter = await _context.Charters.Include(c => c.UnTrustedSkippers).Include(c => c.TrustedSkippers).FirstAsync(x => x.Id == _currentUserService.UserId);
                request.Ids.ToList().ForEach(skipperId =>
                {
                    if (!charter.TrustedSkippers.Select(x => x.SkipperId).Contains(skipperId))
                    {
                        charter.TrustedSkippers.Add(new TrustedCharterSkipper { CharterId = charter.Id, SkipperId = skipperId });
                    }
                    charter.UnTrustedSkippers.RemoveAll(x => x.SkipperId == skipperId);
                });

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
