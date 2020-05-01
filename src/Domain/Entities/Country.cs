using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure.Persistence.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public string EnglishName { get; set; }
        public string TwoLetterCode { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }


    }
}
