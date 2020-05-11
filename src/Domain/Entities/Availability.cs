using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkipperAgency.Domain.Entities
{
    public class Availability
    {
        public int Id { get; set; }
        public string SkipperId { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        [ForeignKey(nameof(SkipperId))]
        public virtual AppUser Skipper { get; set; } 
    } 
}
