using MediatR;
using SkipperAgency.Application.Common.Exceptions;
using SkipperAgency.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Bookings.Commands.CharterDeleteBooking
{
    public class CharterDeleteBookingCommand : IRequest, ICharterBookingAuth
    {
        public int BookingId { get; set; }

        public class Handler : IRequestHandler<CharterDeleteBookingCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<Unit> Handle(CharterDeleteBookingCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Bookings
                    .FindAsync(request.BookingId);

                if (entity is null)
                {
                    throw new NotFoundException(nameof(CharterDeleteBookingCommand), request.BookingId);
                }

                _context.Bookings.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
