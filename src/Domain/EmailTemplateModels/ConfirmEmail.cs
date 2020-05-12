namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class ConfirmEmail : EmailMessageAbstract
    {
        public ConfirmEmail(string toEmail, string fullName, string callbackUrl) : base(toEmail)
        {
            FullName = fullName;
            CallbackUrl = callbackUrl;
        }

        public string FullName { get; }
        public string CallbackUrl { get; }
    }
}
