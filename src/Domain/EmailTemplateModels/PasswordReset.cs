namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class PasswordReset : EmailMessageAbstract
    {
        public PasswordReset(string toEmail, string fullName, string passwordResetUrl) : base(toEmail)
        {
            FullName = fullName;
            PasswordResetUrl = passwordResetUrl;
        }

        public string FullName { get; }
        public string PasswordResetUrl { get; }
    }
}
