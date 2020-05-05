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
    public class DeleteSkipperCommand : IRequest, ISkipperAuth
    {
        public string SkipperId { get; set; }

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
                    .FindAsync(request.SkipperId);

                if (entity is null)
                {
                    throw new NotFoundException(nameof(TodoList), request.SkipperId);
                }

                _context.Skipper.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
