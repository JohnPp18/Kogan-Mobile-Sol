using System.Runtime.Serialization;

namespace Kogan.Mobile.Domain.Mobile.Enums
{
    public enum VoucherCountryEnum
    {
        /// <summary>
        /// None
        /// </summary>
        [EnumMember(Value = "N")]
        None = 0,

        /// <summary>
        /// Australia
        /// </summary>
        [EnumMember(Value = "AU")]
        Australia = 1,

        /// <summary>
        /// New-Zealand
        /// </summary>
        [EnumMember(Value = "NZ")]
        NewZealand = 2
    }
}
