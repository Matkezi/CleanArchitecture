using CleanArchitecture.Domain.Emails;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailWithTemplate(NewSkipperNotice mailTemplate);
        Task SendEmailWithTemplate(ConfirmEmail mailTemplate);
        Task SendEmailWithTemplate(PreRegisteredNotice mailTemplate);
        Task SendEmailWithTemplate(BookingCreated mailTemplate);
    }
}
