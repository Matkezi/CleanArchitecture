using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Skippers.Queries.PreGetSkipper
{
    public class PreGetSkipperQuery : IRequest<PreGetSkipperModel>
    {
        public string Url { get; set; }

        public class Handler : IRequestHandler<PreGetSkipperQuery, PreGetSkipperModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PreGetSkipperModel> Handle(PreGetSkipperQuery request, CancellationToken cancellationToken)
            {
                var skipper = await _context.SkipperPreRegistration.Where(s => s.Url == request.Url).FirstAsync(cancellationToken);
                return _mapper.Map<PreGetSkipperModel>(skipper);
            }

        }
    }
}
