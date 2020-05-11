using SkipperAgency.Domain.Common;
using SkipperAgency.Domain.Enums;
using System;

namespace SkipperAgency.Application.Skippers.Common.Models
{
    public class SkipperLicenseModel : FileModel
    {
        public DateTime DateOfIssue { get; set; }
        public DateTime ValidTo { get; set; }
        public LicenseTypeEnum LicenseType { get; set; }
        public string LicenseUrl { get; set; }
    }
}
