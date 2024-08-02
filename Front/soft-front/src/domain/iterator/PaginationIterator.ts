import type { PaginationResponse } from '@/data/response/PaginationResponse'
import type { PaginationModel } from '@/domain/model/PaginationModel'

export default {

    responseToModel(response : PaginationResponse) : PaginationModel {
        return {
            currentPage:  response.currentPage,
            firstPageURL: response.firstPageUrl,
            from:         0,
            lastPage:     response.lastPage,
            lastPageURL:  response.lastPageUrl,
            nextPageURL:  '',
            path:         response.path,
            perPage:      response.itemsPerPage,
            prevPageURL:  '',
            to:           0,
            total:        response.totalItems,
        } as PaginationModel
    }

}
