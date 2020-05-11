using SkipperAgency.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkipperAgency.Domain.Entities
{
    public class Licence
    {
        public int Id { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime DateOfIssue { get; set; }
        public LicenseTypeEnum LicenceType { get; set; }
        public string LicenceUrl { get; set; }
        public string SkipperId { get; set; }
        [ForeignKey(nameof(SkipperId))]
        public Skipper Skipper { get; set; }
    }
}
