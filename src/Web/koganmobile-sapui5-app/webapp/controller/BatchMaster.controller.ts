import Fragment from "sap/ui/core/Fragment";
import BaseController from "./BaseController";
import Control from "sap/ui/core/Control";
import UI5Element from "sap/ui/core/Element";
import Dialog from "sap/m/Dialog";

/**
 * @namespace kogan.mobile.controller
 */
export default class BatchMaster extends BaseController {
    private _pFragmentImportBatchDialog: Promise<Dialog>;

    public onInit(): void {
    }

    public pressImportNewBatch(): void {
        if (!this._pFragmentImportBatchDialog) {
            this._pFragmentImportBatchDialog = Fragment.load({
                controller: this,
                name: "kogan.mobile.views.fragments.ImportBatchDialog"
            }).then((c) => {
                const dialog = c as Dialog;

                this.getView().addDependent(dialog);

                return dialog;
            });
        }

        this._pFragmentImportBatchDialog.then(dialog => {
            dialog.open();
        })
    }
}
