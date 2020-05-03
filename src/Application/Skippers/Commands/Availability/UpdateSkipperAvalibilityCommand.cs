using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Skippers.Models;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperBooking.Base.Enums;
using SkipperBooking.Web.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Skippers.Commands.UpdateSkipper
{
    public partial class UpdateSkipperAvailabilityCommand : IRequest
    {
        public AvailabilityModel AvailabilityModel { get; set; }
        public class Handler : IRequestHandler<UpdateSkipperAvailabilityCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<Unit> Handle(UpdateSkipperAvailabilityCommand request, CancellationToken cancellationToken)
            {
                Skipper skipper = await _context.Skipper.Include(x => x.Availability).FirstAsync(s => s.Id == _currentUserService.UserId);
                                
                skipper.Availability.ForEach(avalibility => _context.Availabilities.Remove(avalibility));
                skipper.Availability = request.AvailabilityModel.Available
                    .Select(av => new Availability { AvailableFrom = av.From.AddHours(12), AvailableTo = av.To.AddHours(12) }).ToList();
                    
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
