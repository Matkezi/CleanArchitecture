namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class PreRegisteredNotice : EmailMessageAbstract
    {
        public PreRegisteredNotice(string toEmail, string callbackUrl) : base(toEmail)
        {
            CallbackUrl = callbackUrl;
        }
        public string CallbackUrl { get; }
    }
}
