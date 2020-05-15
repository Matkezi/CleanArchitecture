using AutoMapper;
using MediatR;
using SkipperAgency.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Charters.Queries.GetCharter
{
    public class GetCharterQuery : IRequest<CharterModel>
    {
        public string Id { get; set; }

        public class Handler : IRequestHandler<GetCharterQuery, CharterModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CharterModel> Handle(GetCharterQuery request, CancellationToken cancellationToken)
            {
                var charter = await _context.Charters.FindAsync(request.Id);
                return _mapper.Map<CharterModel>(charter);
            }

        }
    }
}
