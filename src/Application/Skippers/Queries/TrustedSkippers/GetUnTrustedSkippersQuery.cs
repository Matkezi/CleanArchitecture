using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Skippers.Queries.TrustedSkippers
{
    public class GetUnTrustedSkippersQuery : IRequest<IEnumerable<TrustedSkipperModel>>
    {

        public class Handler : IRequestHandler<GetUnTrustedSkippersQuery, IEnumerable<TrustedSkipperModel>>
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

            public async Task<IEnumerable<TrustedSkipperModel>> Handle(GetUnTrustedSkippersQuery request, CancellationToken cancellationToken)
            {
                var charter = await _context.Charter.Include(c => c.UnTrustedSkippers).FirstAsync(x => x.Id == _currentUserService.UserId);

                return await _context.Skipper
                    .Include(s => s.ListOfLanguages)
                    .ThenInclude(l => l.Language)
                    .Where(skipper => charter.UnTrustedSkippers.Select(x => x.SkipperID)
                    .Contains(skipper.Id))
                    .ProjectTo<TrustedSkipperModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }

        }
    }
}
