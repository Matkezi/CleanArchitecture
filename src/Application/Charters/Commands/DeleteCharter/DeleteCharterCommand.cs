using MediatR;
using SkipperAgency.Application.Common.Exceptions;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Charters.Commands.DeleteCharter
{
    public class DeleteCharterCommand : IRequest
    {
        public string Id { get; set; }

        public class Handler : IRequestHandler<DeleteCharterCommand>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCharterCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Charter
                    .FindAsync(request.Id);

                if (entity is null)
                {
                    throw new NotFoundException(nameof(DeleteCharterCommand), request.Id);
                }

                _context.Charter.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
