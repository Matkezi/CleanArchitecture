using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Helpers;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.EmailTemplateModels;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Application.Skippers.Commands.PreCreateSkipper
{
    public class PreCreateSkipperCommand : IRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class Handler : IRequestHandler<PreCreateSkipperCommand>
        {
            private readonly IEmailService _emailer;
            private readonly IConfiguration _configuration;
            private readonly IApplicationDbContext _context;
            public Handler(IEmailService emailer, IConfiguration configuration, IApplicationDbContext context)
            {
                _emailer = emailer;
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
                    URL = RandomUrl.GetRandomUrl()
                };
                _context.SkipperPreRegistration.Add(skipper);
                await _context.SaveChangesAsync(cancellationToken);

                await _emailer.SendEmailWithTemplate(
                    new PreRegisteredNotice(
                        toEmail: _configuration["AppSettings:MainCharterEmail"],
                        callbackUrl: skipper.URL
                    ));

                return Unit.Value;
            }
        }
    }
}

