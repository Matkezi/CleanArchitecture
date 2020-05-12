using MediatR;
using SkipperAgency.Application.Common.Exceptions;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Skippers.Commands.DeleteSkipper
{
    public class DeleteSkipperCommand : IRequest
    {
        public string Id { get; set; }

        public class Handler : IRequestHandler<DeleteSkipperCommand>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteSkipperCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Skipper
                    .FindAsync(request.Id);

                if (entity is null)
                {
                    throw new NotFoundException(nameof(Charter), request.Id);
                }

                _context.Skipper.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
