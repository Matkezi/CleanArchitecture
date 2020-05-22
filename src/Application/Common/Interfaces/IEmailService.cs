using SkipperAgency.Domain.EmailTemplateModels;
using System.Threading.Tasks;
using FluentEmail.Core.Models;

namespace SkipperAgency.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailWithTemplate(NewSkipperNoticeModel mailTemplate);
        Task SendEmailWithTemplate(ConfirmEmailModel mailTemplate);
        Task SendEmailWithTemplate(PreRegisteredNoticeModel mailTemplate);
        Task<SendResponse> SendEmailWithTemplate(BookingCreatedModel mailTemplate);
        Task SendEmailWithTemplate(BookingRequestedModel mailTemplate);
        Task SendEmailWithTemplate(SkipperBookingRequestedModel mailTemplate);
        Task SendEmailWithTemplate(SkipperAcceptedModel mailTemplate);
        Task SendEmailWithTemplate(SkipperDeclined mailTemplate);
        Task SendEmailWithTemplate(PasswordResetModel mailTemplate);
        Task SendEmailWithTemplate(ChangeEmailModel mailTemplate);
    }
}
