namespace CleanArchitecture.Infrastructure.Persistence.Entities
{
    public class UnTrustedCharterSkipper
    {
        public string CharterID { get; set; }
        public Charter Charter { get; set; }

        public string SkipperID { get; set; }
        public Skipper Skipper { get; set; }
    }
}
