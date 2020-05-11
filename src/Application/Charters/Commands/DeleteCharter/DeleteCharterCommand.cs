using MediatR;
using SkipperAgency.Application.Common.Exceptions;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Charters.Commands.DeleteCharter
{
    public class DeleteCharterCommand : IRequest, ICharterAuth
    {
        public string CharterId { get; set; }

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
                    .FindAsync(request.CharterId);

                if (entity is null)
                {
                    throw new NotFoundException(nameof(DeleteCharterCommand), request.CharterId);
                }

                _context.Charter.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
