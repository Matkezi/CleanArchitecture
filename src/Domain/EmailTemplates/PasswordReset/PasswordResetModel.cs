namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class PasswordResetModel : EmailMessageAbstract
    {
        public PasswordResetModel(string toEmail, string fullName, string passwordResetUrl) : base(toEmail)
        {
            FullName = fullName;
            PasswordResetUrl = passwordResetUrl;
        }

        public string FullName { get; }
        public string PasswordResetUrl { get; }
    }
}
