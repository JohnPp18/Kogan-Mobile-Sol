using System.Runtime.Serialization;

namespace Kogan.Mobile.Domain.Mobile.Enums
{
    public enum VoucherCountryEnum
    {
        [EnumMember(Value = "N")]
        None = 0,

        [EnumMember(Value = "AU")]
        Australia = 1,

        [EnumMember(Value = "NZ")]
        NewZealand = 2
    }
}
