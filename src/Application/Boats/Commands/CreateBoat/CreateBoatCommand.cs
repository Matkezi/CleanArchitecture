using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using MediatR;
using SkipperBooking.Base.Enums;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Boats.Commands
{
    public class CreateBoatCommand : IRequest
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public BoatTypeEnum Type { get; set; }
        public double Length { get; set; }
        public LicenceTypeEnum MinimalRequiredLicence { get; set; }
        public FileModel BoathPhoto { get; set; }

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
                Boat boat = new Boat
                {
                    Name = request.Name,
                    Manufacturer = request.Manufacturer,
                    Model = request.Model,
                    Type = request.Type,
                    Length = request.Length,
                    MinimalRequiredLicence = request.MinimalRequiredLicence                
                };                

                if (request.BoathPhoto != null)
                {
                    // TODO: validate Data somehow before this, make a validator
                    var photoUri = await _filesStorageService.SaveCloudAsync(request.BoathPhoto.Data, Path.GetExtension(request.BoathPhoto.Name));
                    boat.BoathPhotoUrl = photoUri;
                }

                _context.Boats.Add(boat);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
