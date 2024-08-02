import type { PaginationModel } from '@/domain/model/PaginationModel'
import type { UserModel } from '@/domain/model/auth/UserModel'
import AppRepository from '@/data/repository/AppRepository'
import type { BaseResponse } from '@/data/response/BaseResponse'
import type { UserResponse } from '@/data/response/auth/UserResponse'
import BaseModelIterator from '@/domain/iterator/BaseModelIterator'
import UserIterator from '@/domain/iterator/auth/UserIterator'
import type { BasePaginationResponse } from '@/data/response/BasePaginationResponse'
import PaginationIterator from '@/domain/iterator/PaginationIterator'
import type { BasePaginationModel } from '@/domain/model/BasePaginationModel'
import BasePaginationModelIterator from '@/domain/iterator/BasePaginationModelIterator'
import type { CreateUserModelRequest } from '@/domain/model/app/request/CreateUserModelRequest'
import type { CreateUserRequest } from '@/data/request/app/CreateUserRequest'
import type { BaseModel } from '@/domain/model/BaseModel'
import UserDetailIterator from '@/domain/iterator/app/UserDetailIterator'
import type { CreateUserDetailRequest } from '@/data/request/app/CreateUserDetailRequest'
import type { CreateUserDetailModelRequest } from '@/domain/model/app/request/CreateUserDetailModelRequest'

export default {

    async getUserPaginated(page: number, search?: string) : Promise<BasePaginationModel<UserModel[]>> {
        const response: BaseResponse<BasePaginationResponse<UserResponse[]>> = await AppRepository.getUserPaginated(page, search)

        if(response.data != null && response.data.data != null) {
            const usersModels : UserModel[] = UserIterator.responseToModels(response.data.data)
            const paginator : PaginationModel = PaginationIterator.responseToModel(response.data.pagination)

            return BasePaginationModelIterator.toModel(response.message, usersModels, paginator)
        }

        return BaseModelIterator.toModel("Sin usuarios registrados")
    },

    async newUser(request : CreateUserModelRequest) : Promise<BaseModel<number>> {
        const newRequest : CreateUserRequest = UserIterator.requestModelCreateUserToRequest(request)
        const response : BaseResponse<number> = await AppRepository.newUser(newRequest)

        if (response != null && response.data != null) {
            return BaseModelIterator.toModel<number>(response.message, response.data)
        }

        return BaseModelIterator.toModel(response!.message)
    },

    async getUserById(id: number) : Promise<BaseModel<UserModel>> {
        const response: BaseResponse<UserResponse> = await AppRepository.getUserById(id)

        if(response.data != null) {
            const model : UserModel = UserIterator.responseToModel(response.data)
            return BaseModelIterator.toModel(response.message, model)
        }

        return BaseModelIterator.toModel("Sin registrados")
    },

    async syncUserDetail(request : CreateUserDetailModelRequest) : Promise<BaseModel<number>> {
        const newRequest : CreateUserDetailRequest = UserDetailIterator.requestModelToRequest(request)
        const response : BaseResponse<number> = await AppRepository.syncUserDetail(newRequest)

        if (response != null && response.data != null) {
            return BaseModelIterator.toModel<number>(response.message, response.data)
        }

        return BaseModelIterator.toModel(response!.message)
    },

}
