using Kogan.Domain.BusinessPartners.Enums;

namespace Kogan.Domain.BusinessPartners.Interfaces
{
    public interface IBusinessPartner
    {
        BusinessPartnerTypeEnum Type { get; }
    }
}
