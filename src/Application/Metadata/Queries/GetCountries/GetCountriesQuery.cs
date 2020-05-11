using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Metadata.Queries.GetCountries
{
    public class GetCountriesQuery : IRequest<IEnumerable<CountryModel>>
    {
        public class Handler : IRequestHandler<GetCountriesQuery, IEnumerable<CountryModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CountryModel>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
            {
                return await _context.Countries
                    .ProjectTo<CountryModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }
    }
}
