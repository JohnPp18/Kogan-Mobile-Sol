import { MobileVoucherSimTypeEnum } from "../enums/MobileVoucherSimTypeEnum";
import VoucherPin from "./VoucherPin";

export default class BatchVoucher {
    name: string;
    webSku: string;
    simType: MobileVoucherSimTypeEnum;
    pins: Array<VoucherPin> = [];
}