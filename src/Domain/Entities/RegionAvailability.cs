using System.ComponentModel.DataAnnotations.Schema;

namespace SkipperAgency.Domain.Entities
{
    public class RegionAvailability
    {
        public int Id { get; set; }
        public int AvailabilityId { get; set; }
        public int RegionId { get; set; }
        [ForeignKey(nameof(AvailabilityId))]
        public virtual Availability Availability { get; set; }
        [ForeignKey(nameof(RegionId))]
        public virtual Region Region { get; set; }
    }
}
