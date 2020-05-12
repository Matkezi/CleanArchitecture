namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class NewSkipperNotice : EmailMessageAbstract
    {
        public NewSkipperNotice(string toEmail, string skipperFullName, string skipperEmail) : base(toEmail)
        {
            SkipperFullName = skipperFullName;
            SkipperEmail = skipperEmail;
        }

        public string SkipperFullName { get; }
        public string SkipperEmail { get; }

    }
}
