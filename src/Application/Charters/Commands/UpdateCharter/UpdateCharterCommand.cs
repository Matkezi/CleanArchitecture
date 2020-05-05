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
    public class UpdateCharterCommand : IRequest, ICharterAuth
    {
        public string CharterId { get; set; }
        public string CharterName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }

        public string NewEmail { get; set; }

        public class Handler : IRequestHandler<UpdateCharterCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFilesStorageService _filesStorageService;
            private readonly IIdentityService _identityService;

            public Handler(IApplicationDbContext context, IFilesStorageService filesStorageService, IIdentityService identityService)
            {
                _context = context;
                _filesStorageService = filesStorageService;
                _identityService = identityService;
            }

            public async Task<Unit> Handle(UpdateCharterCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Charter.FirstOrDefaultAsync(x => x.Id == request.CharterId);

                if (entity is null)
                {
                    throw new NotFoundException(nameof(Charter), request.CharterId);
                }

                entity.CharterName = request.CharterName;
                entity.Address = request.Address;
                entity.ZipCode = request.ZipCode;
                entity.City = request.City;
                entity.CountryId = request.CountryId;

                if (!string.IsNullOrEmpty(request.NewEmail))
                {
                    var result = await _identityService.ChangeEmailRequest(entity.UserName, request.NewEmail);
                }              

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
