using Kogan.Mobile.Domain.Mobile.Enums;

namespace Kogan.Mobile.Domain.Mobile
{
    public sealed class VoucherPin
    {
        public int IdBatch { get; set; }

        public int IdVoucher { get; set; }

        public BatchVoucherAssociation BatchVoucherAssociation { get; set; }

        public string PinNumber { get; set; }

        public string Msisdn { get; set; }

        public VoucherPinStateEnum State { get; set; }

        public bool IsSold { get; set; } = false;

        public bool IsRedeemed { get; set; } = false;

        public bool IsExpired { get; set; } = false;
    }
}
