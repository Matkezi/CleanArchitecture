using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkipperAgency.Domain.Entities
{
    public class Skipper : AppUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<TrustedCharterSkipper> TrustedCharters { get; set; }
        public List<UnTrustedCharterSkipper> UnTrustedCharters { get; set; }
        public List<Booking> Bookings { get; set; }
        public float Price { get; set; }

        [NotMapped]
        public string NewEmail { get; set; }
        [NotMapped]
        public string NewPassword { get; set; }
        public Licence Licence { get; set; }

        public List<SkipperSkill> ListOfSkills { get; set; }
        public string PersonalSummary { get; set; }
        public List<Availability> Availability { get; set; }
        public List<SkipperLanguage> ListOfLanguages { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return string.Concat(FirstName, " ", LastName);
            }
        }

        public int YearOfFirstLicence { get; set; }

    }
}
