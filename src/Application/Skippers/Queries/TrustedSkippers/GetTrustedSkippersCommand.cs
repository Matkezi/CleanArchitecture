using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Skippers.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Skippers.Queries.TrustedSkippers
{
    public class GetTrustedSkippersCommand : IRequest<IEnumerable<TrustedSkipperModel>>
    {

        public class Handler : IRequestHandler<GetTrustedSkippersCommand, IEnumerable<TrustedSkipperModel>>
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

            public async Task<IEnumerable<TrustedSkipperModel>> Handle(GetTrustedSkippersCommand request, CancellationToken cancellationToken)
            {
                var charter = await _context.Charter.Include(c => c.TrustedSkippers).FirstAsync(x => x.Id == _currentUserService.UserId);

                return await _context.Skipper
                    .Include(s => s.ListOfLanguages)
                    .ThenInclude(l => l.Language)
                    .Where(skipper => charter.TrustedSkippers.Select(x => x.SkipperID)
                    .Contains(skipper.Id))
                    .ProjectTo<TrustedSkipperModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();                
            }

        }
    }
}
