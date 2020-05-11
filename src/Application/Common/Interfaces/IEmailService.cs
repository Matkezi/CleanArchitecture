using System.Threading.Tasks;
using SkipperAgency.Domain.EmailTemplateModels;

namespace SkipperAgency.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailWithTemplate(NewSkipperNotice mailTemplate);
        Task SendEmailWithTemplate(ConfirmEmail mailTemplate);
        Task SendEmailWithTemplate(PreRegisteredNotice mailTemplate);
        Task SendEmailWithTemplate(BookingCreated mailTemplate);
        Task SendEmailWithTemplate(BookingRequested mailTemplate);
        Task SendEmailWithTemplate(SkipperBookingRequested mailTemplate);
        Task SendEmailWithTemplate(SkipperAccepted mailTemplate);
        Task SendEmailWithTemplate(SkipperDeclined mailTemplate);
        Task SendEmailWithTemplate(PasswordReset mailTemplate);
        Task SendEmailWithTemplate(ChangeEmail mailTemplate);
    }
}
