using SkipperAgency.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkipperAgency.Domain.Entities
{
    public class License
    {
        public int Id { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime DateOfIssue { get; set; }
        public LicenseTypeEnum LicenseType { get; set; }
        public string LicenseUrl { get; set; }
        public string SkipperId { get; set; }
        [ForeignKey(nameof(SkipperId))]
        public Skipper Skipper { get; set; }
    }
}
