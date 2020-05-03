using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Skippers.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Skippers.Queries.TrustedSkippers
{
    public class GetSkipperCommand : IRequest<SkipperModel>
    {
        public string Id { get; set; }

        public class Handler : IRequestHandler<GetSkipperCommand, SkipperModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<SkipperModel> Handle(GetSkipperCommand request, CancellationToken cancellationToken)
            {
                var skipper = await _context.Skipper.Include(s => s.ListOfSkills).ThenInclude(s => s.Skill).Include(s => s.ListOfLanguages).ThenInclude(l => l.Language).Where(s => s.Id == request.Id).FirstAsync();
                return _mapper.Map<SkipperModel>(skipper);
            }

        }
    }
}
