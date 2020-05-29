using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Skippers.Queries.GetSkipper;
using SkipperAgency.Domain.Entities;
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
                var skippers = await _context.Skippers.Include(s => s.ListOfSkills).ThenInclude(sk => sk.Skill)
                                   .Include(s => s.Bookings)
                                   .Include(s => s.Availability)
                                 .Include(sk => sk.ListOfLanguages).ThenInclude(lang => lang.Language)
                                 .Where(skipper =>
                                 (!request.ListOfLanguages.Any() || skipper.ListOfLanguages.Any(lang => request.ListOfLanguages.Any(rl => rl == lang.Language.EnglishName))) &&
                                 skipper.Availability.Any(x => x.AvailableFrom <= request.DateFrom && x.AvailableTo >= request.DateTo) &&
                                 !skipper.Bookings.Any(booking => booking.BookedFrom >= request.DateFrom && booking.BookedFrom <= request.DateTo || (booking.BookedTo >= request.DateFrom && booking.BookedTo <= request.DateTo)))
                                 .OrderByDescending(s => s.ListOfSkills.Select(x => x.Skill.Name).Where(skillName => request.RequiredSkills.Contains(skillName)).Count())
                                 .ProjectTo<SkipperModel>(_mapper.ConfigurationProvider)
                                 .ToListAsync();
                return skippers;
            }

        }
    }
}
