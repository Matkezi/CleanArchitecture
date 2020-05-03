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
    public class GetCharterQuery : IRequest<CharterModel>
    {
        public string Id { get; set; }

        public class Handler : IRequestHandler<GetCharterQuery, CharterModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            
            public async Task<CharterModel> Handle(GetCharterQuery request, CancellationToken cancellationToken)
            {
                var charter = await _context.Charter.FindAsync(request.Id);
                return _mapper.Map<CharterModel>(charter);
            }

        }
    }
}
