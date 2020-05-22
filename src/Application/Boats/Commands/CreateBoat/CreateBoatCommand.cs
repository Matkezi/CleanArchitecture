using MediatR;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Common;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SkipperAgency.Domain.ValueObjects;
using SkipperAgency.Application.Common.Models;

namespace SkipperAgency.Application.Boats.Commands.CreateBoat
{
    public class CreateBoatCommand : IRequest
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public BoatTypeEnum Type { get; set; }
        public double Length { get; set; }
        public LicenseTypeEnum MinimalRequiredLicense { get; set; }
        public FileModel BoatPhoto { get; set; }

        public class Handler : IRequestHandler<CreateBoatCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFilesStorageService _filesStorageService;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IApplicationDbContext context, IFilesStorageService filesStorageService, ICurrentUserService currentUserService)
            {
                _context = context;
                _filesStorageService = filesStorageService;
                _currentUserService = currentUserService;
            }

            public async Task<Unit> Handle(CreateBoatCommand request, CancellationToken cancellationToken)
            {
                var boat = new Boat
                {
                    Name = request.Name,
                    Manufacturer = request.Manufacturer,
                    Model = request.Model,
                    Type = request.Type,
                    Length = request.Length,
                    MinimalRequiredLicense = request.MinimalRequiredLicense,
                    CharterId = _currentUserService.UserId
                };

                if (request.BoatPhoto != null)
                {
                    var photoUri = await _filesStorageService.SaveCloudAsync(request.BoatPhoto.Base64Data, Path.GetExtension(request.BoatPhoto.NameWithExt));
                    boat.BoatPhotoUrl = photoUri;
                }

                await _context.Boats.AddAsync(boat, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
