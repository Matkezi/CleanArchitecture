using MediatR;
using SkipperAgency.Application.Common.Exceptions;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Boats.Commands.DeleteBoat
{
    public class DeleteBoatCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteBoatCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFilesStorageService _filesStorageService;
            public Handler(IApplicationDbContext context, IFilesStorageService filesStorageService)
            {
                _context = context;
                _filesStorageService = filesStorageService;
            }

            public async Task<Unit> Handle(DeleteBoatCommand request, CancellationToken cancellationToken)
            {
                var boat = await _context.Boats
                    .FindAsync(request.Id);

                if (boat is null)
                {
                    throw new NotFoundException(nameof(Boat), request.Id);
                }

                await _filesStorageService.DeleteCloudAsync(boat.BoatPhotoUrl);

                _context.Boats.Remove(boat);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
