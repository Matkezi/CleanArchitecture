using SkipperBooking.Base.Enums;
using System;

namespace CleanArchitecture.Application.Common.Models
{
    public class SkipperLicenceModel : FileModel
    {
        public DateTime DateOfIssue { get; set; }
        public DateTime ValidTo { get; set; }
        public LicenceTypeEnum LicenceType { get; set; }
        public string LicenceUrl { get; set; }
    }
}
