using System;
using SkipperAgency.Domain.Common;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Application.Skippers.Common.Models
{
    public class SkipperLicenceModel : FileModel
    {
        public DateTime DateOfIssue { get; set; }
        public DateTime ValidTo { get; set; }
        public LicenceTypeEnum LicenceType { get; set; }
        public string LicenceUrl { get; set; }
    }
}
