using System.ComponentModel;
using System.Runtime.Serialization;

namespace Kogan.Mobile.Domain.Mobile.Enums
{
    public enum MobileVoucherPlanSizeEnum
    {
        [Description("None")]
        [EnumMember(Value = "N")]
        N = 0,

        [Description("Small")]
        [EnumMember(Value = "S")]
        S = 1,

        [Description("Medium")]
        [EnumMember(Value = "M")]
        M = 2,

        [Description("XLarge")]
        [EnumMember(Value = "XL")]
        XL = 3,

        [Description("Data")]
        [EnumMember(Value = "D")]
        D = 4
    }
}
