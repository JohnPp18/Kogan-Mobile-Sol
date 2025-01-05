export default class PaginatedResponse<T> {
    totalItems: int = 0;
    totalPages: int = 0;
    currentPage: int = 1;
    pageSize: int = 50;
    currentPageItemCount: int = 0;
    items: Array<T> = [];
}