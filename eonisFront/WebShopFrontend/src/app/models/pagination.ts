export interface Pagination<T> {
    totalItemsCount: number
    pageNumber: number
    pageSize: number
    data: T;
}