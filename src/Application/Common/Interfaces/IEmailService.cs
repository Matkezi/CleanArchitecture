using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailWithTemplate(NewSkipperNotice mailTemplate);
        Task SendEmailWithTemplate(ConfirmEmail mailTemplate);
    }
}
