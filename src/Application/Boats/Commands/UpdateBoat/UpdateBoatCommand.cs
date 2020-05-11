using MediatR;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Common;
using SkipperAgency.Domain.Enums;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Boats.Commands.UpdateBoat
{
    public class UpdateBoatCommand : IRequest, ICharterBoatAuth
    {
        public int BoatId { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public BoatTypeEnum Type { get; set; }
        public double Length { get; set; }
        public LicenseTypeEnum MinimalRequiredLicence { get; set; }
        public FileModel BoathPhoto { get; set; }

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
                var boat = await _context.Boats.FindAsync(request.BoatId);

                boat.Name = request.Name;
                boat.Manufacturer = request.Manufacturer;
                boat.Model = request.Model;
                boat.Type = request.Type;
                boat.Length = request.Length;
                boat.MinimalRequiredLicence = request.MinimalRequiredLicence;

                if (request.BoathPhoto != null)
                {
                    var photoUri = await _filesStorageService.ReplaceCloudAsync
                        (request.BoathPhoto.Data,
                        Path.GetExtension(request.BoathPhoto.Name),
                        boat.BoathPhotoUrl);
                    boat.BoathPhotoUrl = photoUri;
                }

                _context.Boats.Add(boat);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
