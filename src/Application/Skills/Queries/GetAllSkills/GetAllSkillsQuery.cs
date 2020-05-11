using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Skills.Queries.GetSkill;

namespace SkipperAgency.Application.Skills.Queries.GetAllSkills
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
