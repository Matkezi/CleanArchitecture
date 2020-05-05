using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Emails;
using CleanArchitecture.Domain.EmailTemplateModels;
using FluentEmail.Core;
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

        public Task SendEmailWithTemplate(PreRegisteredNotice mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Agency New Skipper")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/PreRegisteredNotice.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(BookingCreated mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Created")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/BookingCreated.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(BookingRequested mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Created")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/BookingRequested.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(SkipperAccepted mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Created")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/SkipperBookingRequested.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(SkipperBookingRequested mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Accepted")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/SkipperAccepted.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(SkipperDeclined mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Declined")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/SkipperDeclined.cshtml", mailTemplate)
                .SendAsync();
        }
    }
}
