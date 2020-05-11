using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Skippers.Queries.GetSkipper;

namespace SkipperAgency.Application.Skippers.Queries.GetAllSkippers
{
    public class GetAllSkippersQuery : IRequest<IEnumerable<SkipperModel>>
    {
        public class Handler : IRequestHandler<GetAllSkippersQuery, IEnumerable<SkipperModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<SkipperModel>> Handle(GetAllSkippersQuery request, CancellationToken cancellationToken)
            {
                return await _context.Skipper
                    .Include(s => s.ListOfSkills).ThenInclude(s => s.Skill)
                    .Include(s => s.ListOfLanguages).ThenInclude(l => l.Language)
                    .ProjectTo<SkipperModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }
    }
}
