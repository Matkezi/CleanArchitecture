using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Infrastructure.Persistence.Entities;

namespace CleanArchitecture.Application.Skippers.Models
{
    public class LanguageModel : IMapFrom<SkipperLanguage>
    {
        public int LanguageId { get; set; }
        public string SkipperId { get; set; }
        public string EnglishName { get; set; }
        public string Label { get => EnglishName; } 
        public int LevelOfKnowledge { get; set; }       
    }
}
