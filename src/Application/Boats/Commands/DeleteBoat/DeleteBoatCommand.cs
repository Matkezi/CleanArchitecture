using MediatR;
using SkipperAgency.Application.Common.Exceptions;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Boats.Commands.DeleteBoat
{
    public class DeleteBoatCommand : IRequest, ICharterBoatAuth
    {
        public int BoatId { get; set; }

        public class Handler : IRequestHandler<DeleteBoatCommand>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteBoatCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Boats
                    .FindAsync(request.BoatId);

                if (entity is null)
                {
                    throw new NotFoundException(nameof(Boat), request.BoatId);
                }

                _context.Boats.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
