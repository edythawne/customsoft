import type { PaginationModel } from '@/domain/model/PaginationModel'

export interface BasePaginationModel<T> {
    data? : T;
    message : string;
    pagination? : PaginationModel;
}
