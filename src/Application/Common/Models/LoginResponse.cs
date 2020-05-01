using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Common.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
        public string UserPhotoUrl { get; set; }
    }
}
