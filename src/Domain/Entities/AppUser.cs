using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

//using SkipperBooking.Base.Helpers;

namespace SkipperAgency.Domain.Entities
{
    public abstract class AppUser : IdentityUser
    {
        
        //[ValidOIB(ErrorMessage = "OIB not valid.")]
        public string OIB { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public int? CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        public string UserPhotoUrl { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AppUser user &&
                   OIB == user.OIB;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, NormalizedEmail);
        }
    }
}
