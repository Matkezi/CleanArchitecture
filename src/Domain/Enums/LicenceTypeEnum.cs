using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SkipperBooking.Base.Enums
{
    public enum LicenceTypeEnum
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
