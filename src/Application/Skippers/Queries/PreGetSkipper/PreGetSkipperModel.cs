using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Application.Skippers.Queries.PreGetSkipper
{
    public class PreGetSkipperModel : IMapFrom<PreRegisterSkipper>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
