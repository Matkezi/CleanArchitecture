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
    public class GetTrustedSkippersQuery : IRequest<IEnumerable<TrustedSkipperModel>>
    {

        public class Handler : IRequestHandler<GetTrustedSkippersQuery, IEnumerable<TrustedSkipperModel>>
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

            public async Task<IEnumerable<TrustedSkipperModel>> Handle(GetTrustedSkippersQuery request, CancellationToken cancellationToken)
            {
                var charter = await _context.Charters.Include(c => c.TrustedSkippers).FirstAsync(x => x.Id == _currentUserService.UserId);

                return await _context.Skippers
                    .Include(s => s.ListOfLanguages)
                    .ThenInclude(l => l.Language)
                    .Where(skipper => charter.TrustedSkippers.Select(x => x.SkipperId)
                    .Contains(skipper.Id))
                    .ProjectTo<TrustedSkipperModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }

        }
    }
}
