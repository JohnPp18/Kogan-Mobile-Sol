namespace Kogan.Mobile.Domain.Mobile
{
    public sealed class BatchVoucherAssociation
    {
        public int IdBatch { get; set; }

        public Batch Batch { get; set; }

        public int IdVoucher { get; set; }

        public Voucher Voucher { get; set; }

        public int TotalQuantity { get; set; }

        public ICollection<VoucherPin> Pins { get; set; } = new List<VoucherPin>();
    }
}
