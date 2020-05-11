using SkipperAgency.Domain.Common;
using SkipperAgency.Domain.Enums;
using System;

namespace SkipperAgency.Application.Skippers.Common.Models
{
    public class SkipperLicenceModel : FileModel
    {
        public DateTime DateOfIssue { get; set; }
        public DateTime ValidTo { get; set; }
        public LicenseTypeEnum LicenceType { get; set; }
        public string LicenceUrl { get; set; }
    }
}
