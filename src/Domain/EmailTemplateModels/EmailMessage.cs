namespace SkipperAgency.Domain.EmailTemplateModels
{
    public abstract class EmailMessage
    {
        protected EmailMessage(string toEmail)
        {
            ToEmail = toEmail;
        }
        public string ToEmail { get; }
        public string Subject { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
    }
}
