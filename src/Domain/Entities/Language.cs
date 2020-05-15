using System.Collections.Generic;

namespace SkipperAgency.Domain.Entities
{
    public class Language
    {
        public int Id { get; set; }

        public string EnglishName { get; set; }
        public string TwoLetterCode { get; set; }

        public List<SkipperLanguage> Skippers { get; set; }
    }
}
