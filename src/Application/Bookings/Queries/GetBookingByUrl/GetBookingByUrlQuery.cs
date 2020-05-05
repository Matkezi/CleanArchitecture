using AutoMapper;
using AutoMapper.QueryableExtensions;
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
                    .FirstAsync(b => b.BookingURL == request.Url);
                return _mapper.Map<BookingModel>(booking);

            }

        }
    }
}
