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
                    throw new NotFoundException(nameof(TodoList), request.CharterId);
                }

                _context.Charter.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
