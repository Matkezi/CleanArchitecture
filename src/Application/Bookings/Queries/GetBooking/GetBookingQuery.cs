using AutoMapper;
using MediatR;
using SkipperAgency.Application.Bookings.CommonModels;
using SkipperAgency.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Bookings.Queries.GetBooking
{
    public class GetBookingQuery : IRequest<BookingModel>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetBookingQuery, BookingModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BookingModel> Handle(GetBookingQuery request, CancellationToken cancellationToken)
            {
                var booking = await _context.Bookings.FindAsync(request.Id);
                return _mapper.Map<BookingModel>(booking);
            }

        }
    }
}
