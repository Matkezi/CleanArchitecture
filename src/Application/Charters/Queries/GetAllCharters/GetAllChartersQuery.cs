using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Charters.Queries.GetCharter;
using SkipperAgency.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Charters.Queries.GetAllCharters
{
    public class GetAllChartersQuery : IRequest<IEnumerable<CharterModel>>
    {
        public class Handler : IRequestHandler<GetAllChartersQuery, IEnumerable<CharterModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CharterModel>> Handle(GetAllChartersQuery request, CancellationToken cancellationToken)
            {
                return await _context.Charter
                    .ProjectTo<CharterModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }

        }
    }
}
