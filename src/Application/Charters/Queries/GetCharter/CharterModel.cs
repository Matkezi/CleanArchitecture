using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Application.Metadata.Queries.GetCountries;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Application.Charters.Queries.GetCharter
{
    public class CharterModel : IMapFrom<Charter>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string CharterName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public CountryModel Country { get; set; }
    }
}
