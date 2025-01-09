import BaseController from "./BaseController";

/**
 * @namespace kogan.mobile.controller
 */
export default class AppToolPage extends BaseController {
    public onInit(): void {
    }

    public onPressNavHome(): void {
        this.getRouter().navTo("home");
    }
}
