using SkipperAgency.Domain.Common;
using SkipperAgency.Domain.Enums;
using System;
using SkipperAgency.Domain.ValueObjects;

namespace SkipperAgency.Application.Skippers.Common.Models
{
    public class SkipperLicenseModel : FileModel
    {
        public DateTime DateOfIssue { get; set; }
        public DateTime ValidTo { get; set; }
        public LicenseTypeEnum LicenseType { get; set; }
        public string LicenseUrl { get; set; }

        public SkipperLicenseModel(string nameWithExt, string base64Data) : base(nameWithExt, base64Data)
        {
        }
    }
}
