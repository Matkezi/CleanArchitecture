using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Exceptions;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Identity.Commands.EmailChangeRequest;
using SkipperAgency.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Charters.Commands.UpdateCharter
{
    public class UpdateCharterCommand : IRequest, ICharterAuth
    {
        public string Id { get; set; }
        public string CharterName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }

        public string NewEmail { get; set; }

        public class Handler : IRequestHandler<UpdateCharterCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFilesStorageService _filesStorageService;
            private readonly IIdentityService _identityService;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, IFilesStorageService filesStorageService, IIdentityService identityService, IMediator mediator)
            {
                _context = context;
                _filesStorageService = filesStorageService;
                _identityService = identityService;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(UpdateCharterCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Charter.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity is null)
                {
                    throw new NotFoundException(nameof(Charter), request.Id);
                }

                entity.CharterName = request.CharterName;
                entity.Address = request.Address;
                entity.ZipCode = request.ZipCode;
                entity.City = request.City;
                entity.CountryId = request.CountryId;

                await _context.SaveChangesAsync(cancellationToken);

                if (!string.IsNullOrEmpty(request.NewEmail))
                {
                    _ = _mediator.Send(new EmailChangeRequestCommand { UserEmail = entity.Email, UserNewEmail = request.NewEmail }, cancellationToken);
                }

                return Unit.Value;
            }
        }
    }
}
