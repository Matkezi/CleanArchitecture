using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure.Persistence.Entities
{
    public class SkipperLanguage
    {
        public string SkipperId { get; set; }
        public Skipper Skipper { get; set; }
        public int LanguageId { get; set; }
        public int LevelOfKnowledge { get; set; }
        public Language Language { get; set; }
    }
}
