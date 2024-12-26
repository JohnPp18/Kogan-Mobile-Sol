using Kogan.Mobile.Domain.Mobile.Enums;

namespace Kogan.Mobile.Domain.Mobile.Interfaces
{
    public interface IMobileVoucher
    {
        #region Properties
        int PlanDurationDays { get; set; }

        MobileVoucherPlanSizeEnum PlanSize { get; set; }

        string PinNumber { get; set; }
        #endregion
    }
}
