import MessageBox from "sap/m/MessageBox";
import BaseController from "./BaseController";
import BatchService from "../services/BatchService";
import GetAllQuery from "../models/GetAllQuery";
import Batch from "../models/Batch";
import JSONModel from "sap/ui/model/json/JSONModel";

/**
 * @namespace kogan.mobile.controller
 */
export default class BatchMasterDetailsPage extends BaseController {
    protected batchService: BatchService = new BatchService();
    private getAllQueryBatch: GetAllQuery = new GetAllQuery();

    public sayHello(): void {
        MessageBox.show("Hello World!");
    }

    protected constructor(sName: string) {
        super(sName);
    }

    public override async onInit() {
        let batches = await this.batchService.getAllAsync(this.getAllQueryBatch)
        this.setModel(new JSONModel(batches), "BATCHES");
    }
}
