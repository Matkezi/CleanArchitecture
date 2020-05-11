using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Bookings.CommonModels;
using SkipperAgency.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Bookings.Queries.GetBookingByUrl
{
    public class GetBookingByUrlQuery : IRequest<BookingModel>
    {
        public string Url { get; set; }

        public class Handler : IRequestHandler<GetBookingByUrlQuery, BookingModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BookingModel> Handle(GetBookingByUrlQuery request, CancellationToken cancellationToken)
            {
                var booking = await _context.Bookings
                    .Include(b => b.Boat).Include(b => b.Charter)
                    .Include(b => b.Skipper)
                    .FirstAsync(b => b.BookingUrl == request.Url);
                return _mapper.Map<BookingModel>(booking);

            }

        }
    }
}
