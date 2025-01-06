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
        Small = 1,

        /// <summary>
        /// Medium
        /// </summary>
        [Description("Medium")]
        [EnumMember(Value = "M")]
        Medium = 2,

        /// <summary>
        /// XLarge
        /// </summary>
        [Description("XLarge")]
        [EnumMember(Value = "XL")]
        XLarge = 3,

        /// <summary>
        /// Data
        /// </summary>
        [Description("Data")]
        [EnumMember(Value = "D")]
        Data = 4
    }
}
