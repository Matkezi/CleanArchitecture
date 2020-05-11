using System.ComponentModel.DataAnnotations.Schema;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Domain.Entities
{
    public class Boat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public BoatTypeEnum Type { get; set; }
        public double Length { get; set; }
        public LicenceTypeEnum MinimalRequiredLicence { get; set; }
        public string BoathPhotoUrl { get; set; }
        public string CharterId { get; set; }
        [ForeignKey(nameof(CharterId))]
        public virtual Charter Charter { get; set; }
    }
}
