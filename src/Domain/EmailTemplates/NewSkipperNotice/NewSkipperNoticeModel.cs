namespace SkipperAgency.Domain.EmailTemplateModels
{
    public class NewSkipperNoticeModel : EmailMessageAbstract
    {
        public NewSkipperNoticeModel(string toEmail, string skipperFullName, string skipperEmail) : base(toEmail)
        {
            SkipperFullName = skipperFullName;
            SkipperEmail = skipperEmail;
        }

        public string SkipperFullName { get; }
        public string SkipperEmail { get; }

    }
}
