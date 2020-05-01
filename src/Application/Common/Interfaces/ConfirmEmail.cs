﻿using CleanArchitecture.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public class ConfirmEmail : EmailMessage
    {
        public ConfirmEmail(string toEmail, string fullName, string callbackUrl) : base(toEmail)
        {
            FullName = fullName;
            CallbackUrl = callbackUrl;
        }

        public string FullName { get; }
        public string CallbackUrl { get; }
    }
}
