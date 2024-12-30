using Kogan.Domain.Common.Interfaces;
using Kogan.Domain.SAP.Interfaces;
using Kogan.Mobile.Domain.BusinessPartners;
using Kogan.Mobile.Domain.Common.Interfaces;
using Kogan.Mobile.Domain.Mobile.Enums;

namespace Kogan.Mobile.Domain.Mobile
{
    public sealed class Batch : IEntity, IDescriptive, ISapSynchronizable, IActivable
    {
        #region Properties
        public int Id { get; set; }

        /// <remarks>
        /// Should always be an integer but set as a string just in case this evolves.
        /// </remarks>
        public string SupplierBatchId { get; set; }

        public string Name { get; set; }

        public int TotalQuantity { get; set; }

        /// <summary>
        /// Supplier commission percentage is calculated at batch level.
        /// </summary>
        public decimal SupplierComPrcnt { get; set; }

        public Supplier Supplier { get; set; }

        public int IdSupplier { get; set; }

        public ICollection<BatchVoucherAssociation> Vouchers { get; set; } = new List<BatchVoucherAssociation>();

        public string Description { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public DateTime RedemptionDateEnd { get; set; }

        public MobileVoucherPlanSizeEnum PlanSize { get; set; }

        public VoucherCountryEnum Country { get; set; }

        public int PlanDurationDays { get; set; }

        public string ObjectType { get; set; }

        public string ObjectKey { get; set; }

        public bool Active { get; set; }

        public decimal SalesPrice { get; set; }
        #endregion
    }
}
