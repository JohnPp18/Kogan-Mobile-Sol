using Kogan.Domain.BusinessPartners;
using Kogan.Domain.BusinessPartners.Enums;

namespace Kogan.Mobile.Domain.BusinessPartners
{
    public class Supplier : BusinessPartner
    {
        public decimal DefComPercent { get; set; }

        public Supplier()
             : base(BusinessPartnerTypeEnum.Supplier)
        {
        }
    }
}
