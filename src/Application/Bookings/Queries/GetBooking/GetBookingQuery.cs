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
    public class GetBookingQuery : IRequest<BookingModel>, ICharterOrSkipperBookingAuth
    {
        public int BookingId { get; set; }

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
                var booking = _context.Bookings.FindAsync(request.BookingId);
                return _mapper.Map<BookingModel>(booking);

            }

        }
    }
}
