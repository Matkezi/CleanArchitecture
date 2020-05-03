using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Skippers.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Skippers.Queries.PreGetSkipper
{
    public class GetSkipperQuery : IRequest<PreGetSkipperModel>
    {
        public string Url { get; set; }

        public class Handler : IRequestHandler<GetSkipperQuery, PreGetSkipperModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PreGetSkipperModel> Handle(GetSkipperQuery request, CancellationToken cancellationToken)
            {
                var skipper = await _context.SkipperPreRegistration.Where(s => s.URL == request.Url).FirstAsync();
                return _mapper.Map<PreGetSkipperModel>(skipper);
            }

        }
    }
}
