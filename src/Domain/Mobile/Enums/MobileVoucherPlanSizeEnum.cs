using System.ComponentModel;
using System.Runtime.Serialization;

namespace Kogan.Mobile.Domain.Mobile.Enums
{
    public enum MobileVoucherPlanSizeEnum
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("None")]
        [EnumMember(Value = "N")]
        None = 0,

        /// <summary>
        /// Small
        /// </summary>
        [Description("Small")]
        [EnumMember(Value = "S")]
        S = 1,

        /// <summary>
        /// Medium
        /// </summary>
        [Description("Medium")]
        [EnumMember(Value = "M")]
        M = 2,

        /// <summary>
        /// XLarge
        /// </summary>
        [Description("XLarge")]
        [EnumMember(Value = "XL")]
        XL = 3,

        /// <summary>
        /// Data
        /// </summary>
        [Description("Data")]
        [EnumMember(Value = "D")]
        D = 4
    }
}
