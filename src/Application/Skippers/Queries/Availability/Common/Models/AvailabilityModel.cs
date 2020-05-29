using AutoMapper;
using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SkipperAgency.Application.Skippers.Queries.Availability.Common.Models
{
    public class AvailabilityModel : IMapFrom<Domain.Entities.Availability>
    {
        public IEnumerable<BookingDateRangeModel> Booked { get; set; }
        public IEnumerable<AvailabilityDateRangeModel> Available { get; set; }

    }
}
