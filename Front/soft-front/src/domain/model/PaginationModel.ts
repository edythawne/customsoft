export interface PaginationModel {
    currentPage:  number;
    firstPageURL: string;
    from:         number;
    lastPage:     number;
    lastPageURL:  string;
    nextPageURL:  null | string;
    path:         string;
    perPage:      number;
    prevPageURL:  null | string;
    to:           number;
    total:        number;
}
