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
    public class GetSkillQuery : IRequest<SkillModel>
    {
        public SkillsEnum SkillId { get; set; }

        public class Handler : IRequestHandler<GetSkillQuery, SkillModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<SkillModel> Handle(GetSkillQuery request, CancellationToken cancellationToken)
            {
                var skill = await _context.Skills.FindAsync(request.SkillId);
                return _mapper.Map<SkillModel>(skill);
            }
        }
    }
}
