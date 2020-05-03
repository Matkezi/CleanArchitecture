using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Skippers.Models
{
    public class AvailabilityModel
    {
        public IEnumerable<(DateTime From, DateTime To)> Booked { get; set; }
        public IEnumerable<(DateTime From, DateTime To)> Available { get; set; }
    }
}
