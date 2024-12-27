using Kogan.Domain.BusinessPartners;
using Kogan.Domain.BusinessPartners.Enums;
using Kogan.Mobile.Domain.Mobile.Enums;

namespace Kogan.Mobile.Domain.BusinessPartners
{
    public class Supplier : BusinessPartner
    {
        public decimal DefComPercent { get; set; }

        /// <summary>
        /// The country this supplier provides vouchers to.
        /// </summary>
        public VoucherCountryEnum VoucherCountry { get; set; }

        public Supplier()
             : base(BusinessPartnerTypeEnum.Supplier)
        {
        }
    }
}
