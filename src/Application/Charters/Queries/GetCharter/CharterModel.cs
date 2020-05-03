using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Infrastructure.Persistence.Entities;

namespace CleanArchitecture.Application.Charters.Queries.GetCharter
{
    public class CharterModel : IMapFrom<Charter>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NewEmail { get; set; }
        public string CharterName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public CountryModel Country { get; set; }
    }
}
