using System;
using System.Collections.Generic;
using System.Text;

namespace SkipperAgency.Application.Common.Exceptions
{
    public class ConfirmEmailException : Exception
    {
        public ConfirmEmailException(string email, object errors)
            : base($"Failed to confirm \"{email}\" ({errors}).")
        {
        }
    }
}
