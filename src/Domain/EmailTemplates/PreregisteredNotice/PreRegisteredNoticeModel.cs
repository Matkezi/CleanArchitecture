namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class PreRegisteredNoticeModel : EmailMessageAbstract
    {
        public PreRegisteredNoticeModel(string toEmail, string callbackUrl) : base(toEmail)
        {
            CallbackUrl = callbackUrl;
        }
        public string CallbackUrl { get; }
    }
}
