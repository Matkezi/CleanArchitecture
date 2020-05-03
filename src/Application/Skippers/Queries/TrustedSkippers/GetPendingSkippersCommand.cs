using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Skippers.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Skippers.Queries.TrustedSkippers
{
    public class GetPendingSkippersCommand : IRequest<IEnumerable<TrustedSkipperModel>>
    {

        public class Handler : IRequestHandler<GetPendingSkippersCommand, IEnumerable<TrustedSkipperModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
            {
                _context = context;
                _mapper = mapper;
                _currentUserService = currentUserService;
            }

            public async Task<IEnumerable<TrustedSkipperModel>> Handle(GetPendingSkippersCommand request, CancellationToken cancellationToken)
            {
                var charter = await _context.Charter.Include(c => c.TrustedSkippers).Include(x => x.UnTrustedSkippers).FirstAsync(x => x.Id == _currentUserService.UserId);
                return await _context.Skipper
                    .Include(s => s.ListOfLanguages)
                    .ThenInclude(l => l.Language)
                    .Where(skipper =>
                    !charter.TrustedSkippers.Any(ts => ts.SkipperID == skipper.Id) &&
                    !charter.UnTrustedSkippers.Any(uts => uts.SkipperID == skipper.Id))                    
                    .ProjectTo<TrustedSkipperModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }

        }
    }
}
