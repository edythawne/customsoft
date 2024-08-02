import type { RoleResponse } from '@/data/response/auth/RoleResponse'
import type { RoleModel } from '@/domain/model/auth/RoleModel'

export default {

    responseToModels(response: RoleResponse[]) : RoleModel[] {
        return response.map((item : RoleResponse) => this.responseToModel(item))
    },

    responseToModel(response: RoleResponse) : RoleModel  {
        return {
            id : response.id,
            name : response.name
        } as RoleModel
    }


}
