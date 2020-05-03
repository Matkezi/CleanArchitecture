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
    public class DeleteSkipperCommand : IRequest
    {
        public string Id { get; set; }

        public class DeleteSkipperCommandHandler : IRequestHandler<DeleteSkipperCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteSkipperCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteSkipperCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Skipper
                    .Where(l => l.Id == request.Id)
                    .SingleOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(TodoList), request.Id);
                }

                _context.Skipper.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
