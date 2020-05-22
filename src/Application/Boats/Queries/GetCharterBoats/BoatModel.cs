using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Application.Boats.Queries.GetCharterBoats
{
    public class BoatModel: IMapFrom<Boat>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public BoatTypeEnum Type { get; set; }
        public double Length { get; set; }
        public bool IsActive { get; set; }
        public LicenseTypeEnum MinimalRequiredLicense { get; set; }
        public string BoatPhotoUrl { get; set; }
    }
}
