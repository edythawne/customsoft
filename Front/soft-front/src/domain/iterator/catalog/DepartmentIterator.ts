import type { DepartmentResponse } from '@/data/response/catalog/DepartmentResponse'
import type { DepartmentModel } from '@/domain/model/catalog/DepartmentModel'
import DateHelper from '@/domain/helper/DateHelper'
import AreaIterator from '@/domain/iterator/catalog/AreaIterator'

export default {

    responseToModels(response: DepartmentResponse[]): DepartmentModel[] {
        return response.map((item: DepartmentResponse) => this.responseToModel(item))
    },

    responseToModel(response: DepartmentResponse) : DepartmentModel  {
        const model : DepartmentModel =  {
            id : response.id,
            areaId : response.areaId,
            name : response.name,
            createdAt : DateHelper.fullDateString(response.createdAt),
        }

        if (response.area) {
            model.area =  AreaIterator.responseToModel(response.area)
        }

        return model;
    }


}
