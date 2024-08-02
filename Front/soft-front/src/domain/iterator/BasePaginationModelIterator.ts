import type { PaginationModel } from '@/domain/model/PaginationModel'
import type { BasePaginationModel } from '@/domain/model/BasePaginationModel'

export default {

    toModel<T>(message: string, data?: T, pagination?: PaginationModel) : BasePaginationModel<T> {
        return {
            message : message,
            data : data,
            pagination : pagination
        }
    }

}
