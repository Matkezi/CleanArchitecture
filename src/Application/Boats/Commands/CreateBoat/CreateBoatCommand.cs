using MediatR;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Common;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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

            public Handler(IApplicationDbContext context, IFilesStorageService filesStorageService)
            {
                _context = context;
                _filesStorageService = filesStorageService;
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
                    MinimalRequiredLicense = request.MinimalRequiredLicense
                };

                if (request.BoatPhoto != null)
                {
                    // TODO: validate Data somehow before this, make a validator
                    var photoUri = await _filesStorageService.SaveCloudAsync(request.BoatPhoto.Data, Path.GetExtension(request.BoatPhoto.Name));
                    boat.BoatPhotoUrl = photoUri;
                }

                await _context.Boats.AddAsync(boat, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
