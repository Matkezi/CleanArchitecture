﻿using CleanArchitecture.Domain.Emails;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.EmailTemplateModels
{
    public class PasswordReset : EmailMessage
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
