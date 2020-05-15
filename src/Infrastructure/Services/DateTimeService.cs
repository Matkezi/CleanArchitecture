using SkipperAgency.Application.Common.Interfaces;
using System;

namespace SkipperAgency.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
