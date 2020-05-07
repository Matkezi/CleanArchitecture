using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Boats.Queries.CharterGetBoats;
using CleanArchitecture.Application.Bookings.CommonModels;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Skippers.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperBooking.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Skippers.Queries.Availability
{
    public class CharterGetBooatsQuery : IRequest<IEnumerable<BoatModel>>
    {
        public class Handler : IRequestHandler<CharterGetBooatsQuery, IEnumerable<BoatModel>>
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

            public async Task<IEnumerable<BoatModel>> Handle(CharterGetBooatsQuery request, CancellationToken cancellationToken)
            {
                return await _context.Boats
                    .Where(x => x.CharterId == _currentUserService.UserId)
                    .ProjectTo<BoatModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }

        }
    }
}
