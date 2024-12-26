using Kogan.Domain.Common.Interfaces;
using Kogan.Mobile.Domain.Mobile.Enums;

namespace Kogan.Mobile.Domain.Mobile
{
    public sealed class Voucher : IEntity, IArticle
    {
        public int Id { get; set; }

        public int PlanDurationDays { get; set; }

        public MobileVoucherSimTypeEnum SimType { get; set; }

        public ICollection<BatchVoucherAssociation> Batches { get; set; }

        public string Name { get; set; }

        public string WebSku { get; set; }        
    }
}
