using Kogan.Domain.BusinessPartners;
using Kogan.Domain.BusinessPartners.Enums;

namespace Kogan.Mobile.Domain.BusinessPartners
{
    public class Customer : BusinessPartner
    {
        public Customer()
             : base(BusinessPartnerTypeEnum.Customer)
        {
        }
    }
}
