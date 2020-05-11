using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Skippers.Queries.GetSkipper
{
    public class GetSkipperQuery : IRequest<SkipperModel>
    {
        public string Id { get; set; }

        public class Handler : IRequestHandler<GetSkipperQuery, SkipperModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<SkipperModel> Handle(GetSkipperQuery request, CancellationToken cancellationToken)
            {
                var skipper = await _context.Skipper.Include(s => s.ListOfSkills).ThenInclude(s => s.Skill).Include(s => s.ListOfLanguages).ThenInclude(l => l.Language).Where(s => s.Id == request.Id).FirstAsync();
                return _mapper.Map<SkipperModel>(skipper);
            }

        }
    }
}
