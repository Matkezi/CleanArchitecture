using MediatR;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Common;
using SkipperAgency.Domain.Enums;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SkipperAgency.Application.Common.Exceptions;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.ValueObjects;
using SkipperAgency.Application.Common.Models;

namespace SkipperAgency.Application.Boats.Commands.UpdateBoat
{
    public class UpdateBoatCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public BoatTypeEnum Type { get; set; }
        public double Length { get; set; }
        public LicenseTypeEnum MinimalRequiredLicense { get; set; }
        public FileModel BoatPhoto { get; set; }

        public class Handler : IRequestHandler<UpdateBoatCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFilesStorageService _filesStorageService;

            public Handler(IApplicationDbContext context, IFilesStorageService filesStorageService)
            {
                _context = context;
                _filesStorageService = filesStorageService;
            }

            public async Task<Unit> Handle(UpdateBoatCommand request, CancellationToken cancellationToken)
            {
                var boat = await _context.Boats.FindAsync(request.Id);

                if (boat is null)
                {
                    throw new NotFoundException(nameof(Boat), request.Id);
                }

                boat.Name = request.Name;
                boat.Manufacturer = request.Manufacturer;
                boat.Model = request.Model;
                boat.Type = request.Type;
                boat.Length = request.Length;
                boat.MinimalRequiredLicense = request.MinimalRequiredLicense;

                if (request.BoatPhoto != null)
                {
                    var photoUri = await _filesStorageService.ReplaceCloudAsync
                        (request.BoatPhoto.Base64Data,
                        Path.GetExtension(request.BoatPhoto.NameWithExt),
                        boat.BoatPhotoUrl);
                    boat.BoatPhotoUrl = photoUri;
                }

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
