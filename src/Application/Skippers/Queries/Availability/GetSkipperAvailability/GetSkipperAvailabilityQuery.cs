using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Skippers.Queries.Availability.Common.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Skippers.Queries.Availability.GetSkipperAvailability
{
    public class GetSkipperAvailabilityQuery : IRequest<AvailabilityModel>
    {
        public string SkipperId { get; set; }

        public class Handler : IRequestHandler<GetSkipperAvailabilityQuery, AvailabilityModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AvailabilityModel> Handle(GetSkipperAvailabilityQuery request, CancellationToken cancellationToken)
            {
                var skipper = await _context.Skippers
                    .Include(s => s.Availability)
                    .Include(s => s.Bookings)
                    .FirstAsync(s => s.Id == request.SkipperId, cancellationToken);

                return new AvailabilityModel
                {
                    Available = skipper.Availability.ConvertAll(avalibility => new DateRangeModel { From = avalibility.AvailableFrom, To = avalibility.AvailableTo }),
                    Booked = skipper.Bookings.ConvertAll(booking => new DateRangeModel { From = booking.BookedFrom, To = booking.BookedTo })
                };
            }

        }
    }
}
