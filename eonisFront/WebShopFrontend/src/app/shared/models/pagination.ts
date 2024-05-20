import { IProduct } from "./product"

export interface IPagination {
    items: IProduct[]
    pageNumber: number
    pageSize: number
    totalItemsCount: number
}