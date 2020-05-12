using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Bookings.CommonModels;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Bookings.Queries.GetCharterBookings
{
    public class GetCharterBookingsQuery : IRequest<IEnumerable<BookingModel>>
    {
        public class Handler : IRequestHandler<GetCharterBookingsQuery, IEnumerable<BookingModel>>
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

            public async Task<IEnumerable<BookingModel>> Handle(GetCharterBookingsQuery request, CancellationToken cancellationToken)
            {
                return await _context.Bookings
                    .Include(b => b.Charter).Include(b => b.Boat)
                    .Include(b => b.Skipper).Include(b => b.BookingHistories)
                    .Where(b => b.Charter.Id == _currentUserService.UserId)
                    .ProjectTo<BookingModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }

        }
    }
}
