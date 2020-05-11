namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class ChangeEmail : EmailMessage
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
