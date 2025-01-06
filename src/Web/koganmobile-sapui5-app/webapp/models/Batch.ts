import { MobileVoucherPlanSizeEnum } from "../enums/MobileVoucherPlanSizeEnum";
import { VoucherCountryEnum } from "../enums/VoucherCountryEnum";
import BatchVoucher from "./BatchVoucher";

export default class Batch {
    id: int;
    supplierBatchId: string;
    name: string;
    totalQuantity: int;
    supplierComPrcnt: number;
    planSize: MobileVoucherPlanSizeEnum = MobileVoucherPlanSizeEnum.None;
    country: VoucherCountryEnum = VoucherCountryEnum.Australia;
    planDurationDays: int;
    objectType: string;
    objectKey: string;
    active: boolean;
    salesPrice: number;
    vouchers: Array<BatchVoucher> = [];
    validFrom: Date;
    validTo: Date;
    currencyCode: string = "AUD";
}