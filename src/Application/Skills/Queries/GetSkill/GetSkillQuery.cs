using AutoMapper;
using MediatR;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Skills.Queries.GetSkill
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
