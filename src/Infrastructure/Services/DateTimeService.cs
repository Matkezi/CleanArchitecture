using System;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
