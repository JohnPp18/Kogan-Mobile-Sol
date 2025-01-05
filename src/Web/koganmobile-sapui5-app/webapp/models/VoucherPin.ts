import { VoucherPinStateEnum } from "../enums/VoucherPinStateEnum";

export default class VoucherPin {
    pinNumber: string;
    msisdn: string;
    state: VoucherPinStateEnum = VoucherPinStateEnum.None;
    isSold: boolean = false;
    isRedeemed: boolean = false;
    isExpired: boolean = false;
}