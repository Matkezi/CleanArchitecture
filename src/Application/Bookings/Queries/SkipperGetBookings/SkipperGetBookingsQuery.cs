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
    public class SkipperGetBookingsQuery : IRequest<IEnumerable<BookingModel>>
    {
        public BookingStatusEnum BookingStatus { get; set; }

        public class Handler : IRequestHandler<SkipperGetBookingsQuery, IEnumerable<BookingModel>>
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

            public async Task<IEnumerable<BookingModel>> Handle(SkipperGetBookingsQuery request, CancellationToken cancellationToken)
            {
                return await _context.Bookings
                    .Include(b => b.Skipper).Include(b => b.Boat)
                    .Include(b => b.Charter).Include(b => b.GuestNationality)
                    .Where(b => b.Skipper.Id == _currentUserService.UserId && b.Status == request.BookingStatus)
                    .ProjectTo<BookingModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }

        }
    }
}
