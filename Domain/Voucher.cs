using Kogan.Domain.Common.Interfaces;
using Kogan.Mobile.Domain.Mobile.Enums;

namespace Kogan.Mobile.IntegrationData.Mobile
{
    public sealed class Voucher : IArticle
    {
        public int PlanDurationDays { get; set; }

        public MobileVoucherSimTypeEnum SimType { get; set; }

        public ICollection<BatchVoucherAssociation> Batches { get; set; }

        public string Name { get; set; }

        public string WebSku { get; set; }
    }
}
