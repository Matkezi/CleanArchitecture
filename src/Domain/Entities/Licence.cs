using System;
using System.ComponentModel.DataAnnotations.Schema;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Domain.Entities
{
    public class Licence
    {
        public int Id { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime DateOfIssue { get; set; }
        public LicenceTypeEnum LicenceType { get; set; }
        public string LicenceUrl { get; set; }
        public string SkipperId { get; set; }
        [ForeignKey(nameof(SkipperId))]
        public Skipper Skipper { get; set; }
    }
}
