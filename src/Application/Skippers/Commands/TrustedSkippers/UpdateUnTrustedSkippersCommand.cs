﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Application.Skippers.Commands.TrustedSkippers
{
    public partial class UpdateUnTrustedSkippersCommand : IRequest
    {
        public IEnumerable<string> Ids { get; set; }

        public class Handler : IRequestHandler<UpdateUnTrustedSkippersCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<Unit> Handle(UpdateUnTrustedSkippersCommand request, CancellationToken cancellationToken)
            {
                var charter = await _context.Charter.Include(c => c.UnTrustedSkippers).Include(c => c.TrustedSkippers).FirstAsync(x => x.Id == _currentUserService.UserId);
                request.Ids.ToList().ForEach(skipperId =>
                {
                    if (!charter.UnTrustedSkippers.Select(x => x.SkipperID).Contains(skipperId))
                        charter.UnTrustedSkippers.Add(new UnTrustedCharterSkipper { CharterID = charter.Id, SkipperID = skipperId });

                     charter.TrustedSkippers.RemoveAll(x => x.SkipperID == skipperId);

                });
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
