﻿using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Skippers.Commands.TrustedSkippers
{
    public partial class UpdateTrustedSkippersCommand : IRequest
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
                var charter = await _context.Charter.Include(c => c.UnTrustedSkippers).Include(c => c.TrustedSkippers).FirstAsync(x => x.Id == _currentUserService.UserId);
                request.Ids.ToList().ForEach(skipperId =>
                {
                    if (!charter.TrustedSkippers.Select(x => x.SkipperID).Contains(skipperId))
                    {
                        charter.TrustedSkippers.Add(new TrustedCharterSkipper { CharterID = charter.Id, SkipperID = skipperId });
                    }
                    charter.UnTrustedSkippers.RemoveAll(x => x.SkipperID == skipperId);
                });

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
