using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public class NewSkipperNotice : EmailMessage
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
