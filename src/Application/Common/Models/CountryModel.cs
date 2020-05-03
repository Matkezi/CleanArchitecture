using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Infrastructure.Persistence.Entities;

namespace CleanArchitecture.Application.Common.Models
{
    public class CountryModel : IMapFrom<Country>
    {
        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string TwoLetterCode { get; set; }
        public string Label { get => EnglishName; }
        public string SkipperId { get; set; }
    }
}
