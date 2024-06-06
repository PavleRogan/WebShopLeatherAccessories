export class ShopParams{
    gender: string | undefined;
    category: string | undefined;
    searchPhrase: string | undefined;
    sortBy?: string ='Name';
    sortDirection?: string = 'Ascending';
    pageNumber =1;
    pageSize = 3;
}