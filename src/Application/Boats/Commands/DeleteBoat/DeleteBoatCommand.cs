using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.TodoLists.Commands.DeleteTodoList
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
                    throw new NotFoundException(nameof(TodoList), request.BoatId);
                }

                _context.Boats.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
