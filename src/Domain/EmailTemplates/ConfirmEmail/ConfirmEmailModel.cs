namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class ConfirmEmailModel : EmailMessageAbstract
    {
        public ConfirmEmailModel(string toEmail, string fullName, string callbackUrl) : base(toEmail)
        {
            FullName = fullName;
            CallbackUrl = callbackUrl;
        }

        public string FullName { get; }
        public string CallbackUrl { get; }
    }
}
