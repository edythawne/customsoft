import type { PaginationResponse } from '@/data/response/PaginationResponse'

export interface BasePaginationResponse<T> {

    pagination : PaginationResponse;
    data : T

}
