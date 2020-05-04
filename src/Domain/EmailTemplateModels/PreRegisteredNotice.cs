using CleanArchitecture.Domain.Common;
namespace CleanArchitecture.Domain.Emails
{
    public class PreRegisteredNotice : EmailMessage
    {
        public PreRegisteredNotice(string toEmail, string callbackUrl) : base(toEmail)
        {
            CallbackUrl = callbackUrl;
        }
        public string CallbackUrl { get; }
    }
}
