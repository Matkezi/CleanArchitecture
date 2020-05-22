namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class ChangeEmailModel : EmailMessageAbstract
    {
        public ChangeEmailModel(string toEmail, string fullName, string changeEmailUrl) : base(toEmail)
        {
            FullName = fullName;
            ChangeEmailUrl = changeEmailUrl;
        }

        public string FullName { get; }
        public string ChangeEmailUrl { get; }
    }
}
