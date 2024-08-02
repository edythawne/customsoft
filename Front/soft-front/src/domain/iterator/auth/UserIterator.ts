import type { UserResponse } from '@/data/response/auth/UserResponse'
import type { UserModel } from '@/domain/model/auth/UserModel'
import DateHelper from '@/domain/helper/DateHelper'
import RoleIterator from '@/domain/iterator/auth/RoleIterator'
import DepartmentIterator from '@/domain/iterator/catalog/DepartmentIterator'
import type { CreateUserModelRequest } from '@/domain/model/app/request/CreateUserModelRequest'
import type { CreateUserRequest } from '@/data/request/app/CreateUserRequest'
import UserDetailIterator from '@/domain/iterator/app/UserDetailIterator'

export default {

    responseToModels(response : UserResponse[]) : UserModel[] {
        return response.map((item: UserResponse) => this.responseToModel(item))
    },

    responseToModel(response : UserResponse) : UserModel {
        const model : UserModel = {
            id : response.id,
            name : response.name,
            last_name : response.lastName,
            email : response.email,
            token : response.token ?? '',
            created_at : DateHelper.fullDateString(response.createdAt),
            department : DepartmentIterator.responseToModel(response.department)
        } as UserModel

        if (response.roles){
            model.roles = RoleIterator.responseToModels(response.roles)
        }

        if (response.detail) {
            model.detail = UserDetailIterator.responseToModel(response.detail)
        }

        return model
    },

    requestModelCreateUserToRequest(model : CreateUserModelRequest) : CreateUserRequest {
        return {
            name : model.name,
            firstName : model.first_name,
            email : model.email,
            fkRol : model.role?.id,
            fkDepartment : model.department?.id
        } as CreateUserRequest
    }


}
