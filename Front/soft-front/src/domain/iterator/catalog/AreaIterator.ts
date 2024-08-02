import type { AreaModel } from '@/domain/model/catalog/AreaModel'
import type { AreaResponse } from '@/data/response/catalog/AreaResponse'
import DateHelper from '@/domain/helper/DateHelper'


export default {

    responseToModel(response: AreaResponse) : AreaModel  {
        return {
            id : response.id,
            name : response.name,
            createdAt : DateHelper.fullDateString(response.createdAt)
        } as AreaModel
    }


}
