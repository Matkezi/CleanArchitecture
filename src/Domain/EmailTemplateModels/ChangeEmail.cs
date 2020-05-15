namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class ChangeEmail : EmailMessageAbstract
    {
        public ChangeEmail(string toEmail, string fullName, string changeEmailUrl) : base(toEmail)
        {
            FullName = fullName;
            ChangeEmailUrl = changeEmailUrl;
        }

        public string FullName { get; }
        public string ChangeEmailUrl { get; }
    }
}
