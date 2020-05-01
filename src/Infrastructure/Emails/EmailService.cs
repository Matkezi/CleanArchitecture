using CleanArchitecture.Application.Common.Interfaces;
using FluentEmail.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public Task SendEmailWithTemplate(NewSkipperNotice mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"{mailTemplate.SkipperFullName} just joined Skipper Agency")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/NewSkipperNotice.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(ConfirmEmail mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Welcome to Skipper Agency")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/ConfirmEmail.cshtml", mailTemplate)
                .SendAsync();
        }
    }
}
