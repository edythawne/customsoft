import type { BaseResponse } from '@/data/response/BaseResponse'
import BaseModelIterator from '@/domain/iterator/BaseModelIterator'
import type { RoleModel } from '@/domain/model/auth/RoleModel'
import type { RoleResponse } from '@/data/response/auth/RoleResponse'
import RoleIterator from '@/domain/iterator/auth/RoleIterator'
import AuthRepository from '@/data/repository/AuthRepository'
import type { BaseModel } from '@/domain/model/BaseModel'

export default {

    async getAll() : Promise<BaseModel<RoleModel[]>> {
        const response: BaseResponse<RoleResponse[]> = await AuthRepository.getAllRole()

        if(response.data != null) {
            const models : RoleModel[] = RoleIterator.responseToModels(response.data)
            return BaseModelIterator.toModel(response.message, models)
        }

        return BaseModelIterator.toModel("Sin registrados")
    }


}
