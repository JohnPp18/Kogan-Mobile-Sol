using System.Runtime.Serialization;

namespace Kogan.Mobile.Domain.Mobile.Enums
{
    public enum MobileVoucherSimTypeEnum
    {
        [EnumMember(Value = "None")]
        None = 0,

        [EnumMember(Value = "NoSim")]
        NoSim = 1,

        [EnumMember(Value = "Sim")]
        Sim = 2,

        [EnumMember(Value = "ESim")]
        ESim = 3
    }
}
