namespace Kogan.Mobile.IntegrationData.Mobile
{
    public sealed class VoucherPin
    {
        public int IdBatch { get; set; }

        public int IdVoucher { get; set; }

        public BatchVoucherAssociation BatchVoucherAssociation { get; set; }

        public string PinNumber { get; set; }

        public string Msisdn { get; set; }

        public bool IsSold { get; set; } = false;

        public bool IsRedeemed { get; set; } = false;

        public bool IsExpired { get; set; } = false;
        /*
        public bool IsRefunded { get; set; } = false;

        public decimal? RefundedAmount { get; set; }
        */
    }
}
