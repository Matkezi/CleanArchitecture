﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Skippers.Queries.GetSkipper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Skippers.Queries.Availability.GetAvailableSkippers
{
    public class GetAvailableSkippersQuery : IRequest<IEnumerable<SkipperModel>>
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<string> RequiredSkills { get; set; }
        public List<string> ListOfLanguages { get; set; }

        public class Handler : IRequestHandler<GetAvailableSkippersQuery, IEnumerable<SkipperModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<SkipperModel>> Handle(GetAvailableSkippersQuery request, CancellationToken cancellationToken)
            {
                return await _context.Skippers.Include(s => s.ListOfSkills).ThenInclude(sk => sk.Skill).Include(s => s.Bookings).Include(s => s.Availability)
                  .Include(sk => sk.ListOfLanguages).ThenInclude(lang => lang.Language)
                .Where(skipper => request.ListOfLanguages.Count == 0 || skipper.ListOfLanguages.ConvertAll(lang => lang.Language.EnglishName).Intersect(request.ListOfLanguages).Any())
                .Where(skipper => skipper.Availability.Any(av => av.AvailableFrom <= request.DateFrom && av.AvailableTo >= request.DateTo))
                .Where(skipper => !skipper.Bookings.Any(book => (book.BookedFrom >= request.DateFrom && book.BookedFrom <= request.DateTo) ||
                                                        (book.BookedTo >= request.DateFrom && book.BookedTo <= request.DateTo)))
                .OrderByDescending(skipper => skipper.ListOfSkills.ConvertAll(s => s.Skill.Name).FindAll(s => request.RequiredSkills.Contains(s)).Count)
                .ProjectTo<SkipperModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            }

        }
    }
}
