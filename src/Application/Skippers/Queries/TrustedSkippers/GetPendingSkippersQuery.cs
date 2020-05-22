using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Skippers.Queries.TrustedSkippers
{
    public class GetPendingSkippersQuery : IRequest<IEnumerable<TrustedSkipperModel>>
    {

        public class Handler : IRequestHandler<GetPendingSkippersQuery, IEnumerable<TrustedSkipperModel>>
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

            public async Task<IEnumerable<TrustedSkipperModel>> Handle(GetPendingSkippersQuery request, CancellationToken cancellationToken)
            {
                return await _context.Skippers
                    .Include(s => s.ListOfLanguages)
                    .ThenInclude(l => l.Language)
                    .Include(s => s.TrustedCharters)
                    .Include(s => s.UnTrustedCharters)
                    .Where(s => s.UnTrustedCharters.All(uts => uts.SkipperId != s.Id) 
                    && s.TrustedCharters.All(ts => ts.SkipperId != s.Id))
                    .ProjectTo<TrustedSkipperModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();


            }

        }
    }
}
