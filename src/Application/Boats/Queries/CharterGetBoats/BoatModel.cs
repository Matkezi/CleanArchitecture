using SkipperBooking.Base.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Boats.Queries.CharterGetBoats
{
    public class BoatModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public BoatTypeEnum Type { get; set; }
        public double Length { get; set; }
        public LicenceTypeEnum MinimalRequiredLicence { get; set; }
        public string BoathPhotoUrl { get; set; }
    }
}
