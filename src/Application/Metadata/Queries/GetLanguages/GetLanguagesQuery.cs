using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Metadata.Queries.GetLanguages
{
    public class GetLanguagesQuery : IRequest<IEnumerable<LanguageModel>>
    {
        public class Handler : IRequestHandler<GetLanguagesQuery, IEnumerable<LanguageModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<LanguageModel>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
            {
                return await _context.Languages
                    .ProjectTo<LanguageModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }
    }
}
