using System.Runtime.Serialization;

namespace Kogan.Mobile.Domain.Mobile.Enums
{
    public enum VoucherPinStateEnum
    {
        [EnumMember(Value = "None")]
        None = 0,

        [EnumMember(Value = "A")]
        A = 1
    }
}
