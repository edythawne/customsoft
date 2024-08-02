import type { DepartmentModel } from '@/domain/model/catalog/DepartmentModel'
import CatalogRepository from '@/data/repository/CatalogRepository'
import DepartmentIterator from '@/domain/iterator/catalog/DepartmentIterator'
import type { BaseResponse } from '@/data/response/BaseResponse'
import BaseModelIterator from '@/domain/iterator/BaseModelIterator'
import type { DepartmentResponse } from '@/data/response/catalog/DepartmentResponse'
import type { BaseModel } from '@/domain/model/BaseModel'

export default {

    async getAll() : Promise<BaseModel<DepartmentModel[]>> {
        const response: BaseResponse<DepartmentResponse[]> = await CatalogRepository.getAllDepartment()

        if(response.data != null) {
            const models : DepartmentModel[] = DepartmentIterator.responseToModels(response.data)
            return BaseModelIterator.toModel(response.message, models)
        }

        return BaseModelIterator.toModel("Sin registrados")
    }

}
