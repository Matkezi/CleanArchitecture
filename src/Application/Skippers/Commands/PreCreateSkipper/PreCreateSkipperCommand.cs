using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.EmailTemplateModels;
using SkipperAgency.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using SkipperAgency.Application.Common.ExtensionMethods;

namespace SkipperAgency.Application.Skippers.Commands.PreCreateSkipper
{
    public class PreCreateSkipperCommand : IRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class Handler : IRequestHandler<PreCreateSkipperCommand>
        {
            private readonly IEmailService _emailService;
            private readonly IConfiguration _configuration;
            private readonly IApplicationDbContext _context;
            public Handler(IEmailService emailService, IConfiguration configuration, IApplicationDbContext context)
            {
                _emailService = emailService;
                _configuration = configuration;
                _context = context;
            }

            public async Task<Unit> Handle(PreCreateSkipperCommand request, CancellationToken cancellationToken)
            {
                var skipper = new PreRegisterSkipper
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Url = RandomUrl.GetRandomUrl()
                };
                await _context.SkipperPreRegistration.AddAsync(skipper, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await _emailService.SendEmailWithTemplate(
                    new PreRegisteredNotice(
                        toEmail: _configuration["AppSettings:MainCharterEmail"],
                        callbackUrl: skipper.Url
                    ));

                return Unit.Value;
            }
        }
    }
}

