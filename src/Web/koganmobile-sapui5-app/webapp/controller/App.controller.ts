import JSONModel from "sap/ui/model/json/JSONModel";
import BaseController from "./BaseController";
import PageModel from "../models/PageModel";

/**
 * @namespace kogan.mobile.controller
 */
export default class App extends BaseController {
	public onInit(): void {
		// apply content density mode to root view
		this.getView().addStyleClass(this.getOwnerComponent().getContentDensityClass());

		this.setModel(new JSONModel(new PageModel()), "PAGE");
	}
}
