export interface PaginationResponse {
    currentPage:                number;
    firstPageUrl:               string;
    lastPageUrl:                string;
    currentPageUrl:             string;
    totalItems:                 number;
    itemsPerPage:               number;
    path:                       string;
    query:                      string;
    lastPage:                   number;
    totalItemsPerPageCurrent:   number;
    hasPreviousPage:            boolean;
    hasNextPage:                boolean;
}
