using SkipperBooking.Base.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CleanArchitecture.Infrastructure.Persistence.Entities
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
