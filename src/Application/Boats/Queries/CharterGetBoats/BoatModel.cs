using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Application.Boats.Queries.CharterGetBoats
{
    public class BoatModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public BoatTypeEnum Type { get; set; }
        public double Length { get; set; }
        public LicenseTypeEnum MinimalRequiredLicense { get; set; }
        public string BoatPhotoUrl { get; set; }
    }
}
