using System.Runtime.Serialization;

namespace Kogan.Domain.BusinessPartners.Enums
{
    public enum BusinessPartnerTypeEnum
    {
        [EnumMember(Value = "N")]
        None = 0,

        [EnumMember(Value = "C")]
        Customer = 1,

        [EnumMember(Value = "S")]
        Supplier = 2,

        [EnumMember(Value = "L")]
        Lead = 3
    }
}
