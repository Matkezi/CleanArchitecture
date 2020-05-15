using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SkipperAgency.Application.Common.Exceptions;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteBookingCommand>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
            {
                var booking = await _context.Bookings
                    .FindAsync(request.Id);

                if (booking is null)
                {
                    throw new NotFoundException(nameof(DeleteBookingCommand), request.Id);
                }

                _context.Bookings.Remove(booking);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
