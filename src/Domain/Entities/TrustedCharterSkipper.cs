namespace SkipperAgency.Domain.Entities
{
    public class TrustedCharterSkipper
    {
        public string CharterId { get; set; }
        public Charter Charter { get; set; }

        public string SkipperId { get; set; }
        public Skipper Skipper { get; set; }
    }
}
