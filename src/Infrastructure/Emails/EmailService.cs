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
                .UsingTemplateFromFile("../Domain/EmailTemplates/NewSkipperNotice/NewSkipperNoticeTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(ConfirmEmailModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Welcome to Skipper Agency")
                .UsingTemplateFromFile("../Domain/EmailTemplates/ConfirmEmail/ConfirmEmailTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(PreRegisteredNoticeModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Agency New Skipper")
                .UsingTemplateFromFile("../Domain/EmailTemplates/PreRegisteredNotice/PreRegisteredNoticeTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(BookingCreatedModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Created")
                .UsingTemplateFromFile("../Domain/EmailTemplates/BookingCreated/BookingCreatedTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(BookingRequestedModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Requested")
                .UsingTemplateFromFile("../Domain/EmailTemplates/BookingRequested/BookingRequestedTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(SkipperAcceptedModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Accepted")
                .UsingTemplateFromFile("../Domain/EmailTemplates/SkipperBookingRequested/SkipperBookingRequestedTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(SkipperBookingRequestedModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Accepted")
                .UsingTemplateFromFile("../Domain/EmailTemplates/SkipperAccepted/SkipperAcceptedTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(SkipperDeclined mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject($"Skipper Booking Declined")
                .UsingTemplateFromFile("../Domain/EmailTemplates/SkipperDeclined/SkipperDeclinedTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(PasswordResetModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject("Skipper Agency Password Reset")
                .UsingTemplateFromFile("../Domain/EmailTemplates/PasswordReset/PasswordResetTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(ChangeEmailModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .CC(mailTemplate.Cc)
                .BCC(mailTemplate.Bcc)
                .Subject("Skipper Agency Change Email")
                .UsingTemplateFromFile("../Domain/EmailTemplates/ChangeEmail/ChangeEmailTemplate.cshtml", mailTemplate)
                .SendAsync();
        }
    }
}
