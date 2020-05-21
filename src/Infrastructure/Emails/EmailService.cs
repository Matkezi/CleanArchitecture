using FluentEmail.Core;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.EmailTemplateModels;
using System.Threading.Tasks;

namespace SkipperAgency.Infrastructure.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public Task SendEmailWithTemplate(NewSkipperNoticeModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"{mailTemplate.SkipperFullName} just joined Skipper Agency")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/NewSkipperNotice.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(ConfirmEmailModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Welcome to Skipper Agency")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/ConfirmEmail.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(PreRegisteredNoticeModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Agency New Skipper")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/PreRegisteredNotice.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(BookingCreatedModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Created")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/BookingCreated.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(BookingRequestedModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Requested")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/BookingRequested.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(SkipperAcceptedModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Accepted")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/SkipperBookingRequested.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(SkipperBookingRequestedModel mailTemplate)
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

        public Task SendEmailWithTemplate(PasswordResetModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject("Skipper Agency Password Reset")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/PasswordReset.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(ChangeEmailModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject("Skipper Agency Change Email")
                .UsingTemplateFromFile("./wwwroot/Templates/Emails/ChangeEmail.cshtml", mailTemplate)
                .SendAsync();
        }
    }
}
