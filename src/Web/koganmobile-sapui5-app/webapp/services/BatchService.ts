import Batch from "../models/Batch";
import PaginatedResponse from "../models/PaginatedResponse";
import KoganService from "./KoganService";
import GetAllQuery from "../models/GetAllQuery";
import GetAllService from "./IGetAllService";
import axios from "axios";
import IGetSingleEntityService from "./IGetSingleEntityService";
import { API_URL } from "../Constants";

export default class BatchService extends KoganService implements GetAllService<Batch>, IGetSingleEntityService<Batch> {
    private static _ENDPOINT_: string = "Batches";

    async getAllAsync(query: GetAllQuery): Promise<PaginatedResponse<Batch>> {
        const axiosResponse = await axios.get(this.urlJoin(API_URL, BatchService._ENDPOINT_), {
            params: {
                page: query.page,
                pageSize: query.pageSize
            }
        });
        return axiosResponse.data;
    }

    async getSingleAsync(id: int): Promise<Batch> {
        const axiosResponse = await axios.get(this.urlJoin(API_URL, BatchService._ENDPOINT_ + `/${id}`));
        return axiosResponse.data;
    }
}