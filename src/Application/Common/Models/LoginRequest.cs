using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Common.Models
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
