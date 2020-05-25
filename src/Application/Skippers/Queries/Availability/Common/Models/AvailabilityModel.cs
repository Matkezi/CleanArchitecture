using AutoMapper;
using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SkipperAgency.Application.Skippers.Queries.Availability.Common.Models
{
    public class AvailabilityModel : IMapFrom<SkipperAgency.Domain.Entities.Availability>
    {
        public IEnumerable<DateRangeModel> Booked { get; set; }
        public IEnumerable<DateRangeModel> Available { get; set; }

    }
}
