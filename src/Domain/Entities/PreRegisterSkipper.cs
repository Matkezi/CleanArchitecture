using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure.Persistence.Entities
{
    public class PreRegisterSkipper
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
    }
}
