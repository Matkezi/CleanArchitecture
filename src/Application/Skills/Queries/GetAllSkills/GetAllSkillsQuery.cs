using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Skippers.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperBooking.Base.Enums;
using SkipperBooking.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Skippers.Queries.GetSkipper
{
    public class GetAllSkillsQuery : IRequest<IEnumerable<SkillModel>>
    {
        public class Handler : IRequestHandler<GetAllSkillsQuery, IEnumerable<SkillModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<SkillModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
            {
                return await _context.Skills
                    .ProjectTo<SkillModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }
    }
}
