using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Boats.Queries.GetCharterBoats
{
    public class GetCharterBoatsQuery : IRequest<IEnumerable<BoatModel>>
    {
        public class Handler : IRequestHandler<GetCharterBoatsQuery, IEnumerable<BoatModel>>
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

            public async Task<IEnumerable<BoatModel>> Handle(GetCharterBoatsQuery request, CancellationToken cancellationToken)
            {
                return await _context.Boats
                    .Where(x => x.CharterId == _currentUserService.UserId)
                    .ProjectTo<BoatModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }

        }
    }
}
