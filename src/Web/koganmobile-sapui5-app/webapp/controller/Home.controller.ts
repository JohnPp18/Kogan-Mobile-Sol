import BaseController from "./BaseController";

/**
 * @namespace kogan.mobile.controller
 */
export default class Home extends BaseController {
    public onInit(): void {
    }

    public onPressNavToBatch(): void {
        this.getRouter().navTo("batches");
    }
}
