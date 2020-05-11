using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Charters.Queries.GetCharter;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Skippers.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Skippers.Queries.GetSkipper
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
