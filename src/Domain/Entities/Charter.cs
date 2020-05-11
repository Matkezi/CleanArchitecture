using System.Collections.Generic;

namespace SkipperAgency.Domain.Entities
{
    public class Charter : AppUser
    {
        public string CharterName { get; set; }

        public List<TrustedCharterSkipper> TrustedSkippers { get; set; }
        public List<UnTrustedCharterSkipper> UnTrustedSkippers { get; set; }


        public List<Booking> Bookings { get; set; }
    }
}
