using SkipperAgency.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

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
        public LicenseTypeEnum MinimalRequiredLicense { get; set; }
        public string BoatPhotoUrl { get; set; }
        public string CharterId { get; set; }
        [ForeignKey(nameof(CharterId))]
        public virtual Charter Charter { get; set; }
    }
}
