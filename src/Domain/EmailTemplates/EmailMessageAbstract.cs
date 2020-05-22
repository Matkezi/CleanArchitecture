namespace SkipperAgency.Domain.EmailTemplateModels
{
    public abstract class EmailMessageAbstract
    {
        protected EmailMessageAbstract(string toEmail)
        {
            ToEmail = toEmail;
        }
        public string ToEmail { get; }
        public string Subject { get; set; }
    }
}
