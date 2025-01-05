import GetAllQuery from "../models/GetAllQuery";
import PaginatedResponse from "../models/PaginatedResponse";

export default interface GetAllService<T> {
    getAllAsync(query: GetAllQuery): Promise<PaginatedResponse<T>>;
}