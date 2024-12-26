using System.Runtime.Serialization;

namespace Kogan.Mobile.Domain.Mobile.Enums
{
    public enum MobileVoucherSimTypeEnum
    {
        [EnumMember(Value = "NoSim")]
        NoSim = 0,

        [EnumMember(Value = "Sim")]
        Sim = 1,

        [EnumMember(Value = "ESim")]
        ESim = 2
    }
}
