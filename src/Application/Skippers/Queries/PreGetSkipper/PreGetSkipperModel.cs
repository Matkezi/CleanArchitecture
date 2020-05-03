using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Infrastructure.Persistence.Entities;

namespace CleanArchitecture.Application.Skippers.Models
{
    public class PreGetSkipperModel : IMapFrom<PreRegisterSkipper>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
