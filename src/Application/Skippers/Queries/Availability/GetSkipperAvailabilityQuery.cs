﻿using AutoMapper;
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

namespace CleanArchitecture.Application.Skippers.Queries.Availability
{
    public class GetSkipperAvailabilityQuery : IRequest<AvailabilityModel>
    {
        public string Id { get; set; }

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
                var skipper = await _context.Skipper.Include(s => s.Availability).Include(s => s.Bookings).Where(s => s.Id == request.Id).FirstAsync();

                // TODO: test this, if it doesn't work go with the one below.
                return _mapper.Map<AvailabilityModel>(skipper);

                return new AvailabilityModel
                {
                    Available = skipper.Availability.Select(avalibility => (From: avalibility.AvailableFrom, To: avalibility.AvailableTo)),
                    Booked = skipper.Bookings.Select(booking => ( From: booking.BookedFrom, To: booking.BookedTo))
                };
            }

        }
    }
}
