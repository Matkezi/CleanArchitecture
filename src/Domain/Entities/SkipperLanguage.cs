namespace SkipperAgency.Domain.Entities
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
