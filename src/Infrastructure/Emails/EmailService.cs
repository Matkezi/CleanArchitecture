using FluentEmail.Core;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.EmailTemplateModels;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace SkipperAgency.Infrastructure.Emails
{
    public class EmailService: IEmailService
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
                .Subject($"{mailTemplate.SkipperFullName} just joined Skipper Agency")
                .UsingTemplateFromFile($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/EmailTemplates/NewSkipperNotice/NewSkipperNoticeTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(ConfirmEmailModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .Subject($"Welcome to Skipper Agency")
                .UsingTemplateFromFile($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/EmailTemplates/ConfirmEmail/ConfirmEmailTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(PreRegisteredNoticeModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .Subject($"Skipper Agency New Skipper")
                .UsingTemplateFromFile($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/EmailTemplates/PreRegisteredNotice/PreRegisteredNoticeTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(BookingCreatedModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .Subject($"Skipper Booking Created")
                .UsingTemplateFromFile($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/EmailTemplates/BookingCreated/BookingCreatedTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(BookingRequestedModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .Subject($"Skipper Booking Requested")
                .UsingTemplateFromFile($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/EmailTemplates/BookingRequested/BookingRequestedTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(SkipperAcceptedModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .Subject($"Skipper Booking Accepted")
                .UsingTemplateFromFile($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/EmailTemplates/SkipperAccepted/SkipperAcceptedTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(SkipperBookingRequestedModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .Subject($"Skipper Booking Accepted")
                .UsingTemplateFromFile($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/EmailTemplates/SkipperBookingRequested/SkipperBookingRequestedTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(SkipperDeclined mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .Subject($"Skipper Booking Declined")
                .UsingTemplateFromFile($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/EmailTemplates/SkipperDeclined/SkipperDeclinedTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(PasswordResetModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .Subject("Skipper Agency Password Reset")
                .UsingTemplateFromFile($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/EmailTemplates/PasswordReset/PasswordResetTemplate.cshtml", mailTemplate)
                .SendAsync();
        }

        public Task SendEmailWithTemplate(ChangeEmailModel mailTemplate)
        {
            return _fluentEmail
                .To(mailTemplate.ToEmail)
                .Subject("Skipper Agency Change Email")
                .UsingTemplateFromFile($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/EmailTemplates/ChangeEmail/ChangeEmailTemplate.cshtml", mailTemplate)
                .SendAsync();
        }
    }
}
