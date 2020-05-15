using System.ComponentModel;

namespace SkipperAgency.Domain.Enums
{
    public enum LicenseTypeEnum
    {
        [Description("C category")]
        CCategory = 0,
        [Description("B category")]
        BCategory = 1,
        [Description("Yacht master A")]
        YachtMasterA = 2,
        [Description("Yacht master B")]
        YachtMasterB = 3
    }
}
