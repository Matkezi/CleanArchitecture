using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Skippers.Models1;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Skippers.Queries.GetSkipper
{
    public class GetLanguagesQuery : IRequest<IEnumerable<CountryModel>>
    {
        public class Handler : IRequestHandler<GetLanguagesQuery, IEnumerable<CountryModel>>
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
