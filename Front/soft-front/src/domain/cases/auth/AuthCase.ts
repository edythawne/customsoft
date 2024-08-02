import type { BaseModel } from '@/domain/model/BaseModel'
import type { UserModel } from '@/domain/model/auth/UserModel'
import type { UserResponse } from '@/data/response/auth/UserResponse'
import AuthRepository from '@/data/repository/AuthRepository'
import type { LoginRequest } from '@/data/request/auth/LoginRequest'
import AuthIterator from '@/domain/iterator/auth/AuthIterator'
import type { LoginModelRequest } from '@/domain/model/auth/request/LoginModelRequest'
import UserIterator from '@/domain/iterator/auth/UserIterator'
import BaseModelIterator from '@/domain/iterator/BaseModelIterator'
import type { BaseResponse } from '@/data/response/BaseResponse'

export default {

    async login(requestModel : LoginModelRequest) : Promise<BaseModel<UserModel>>{
        const request : LoginRequest = AuthIterator.loginModelToRequest(requestModel)
        const response : BaseResponse<UserResponse> = await AuthRepository.login(request)

        if (response.data != null) {
            const model: UserModel = UserIterator.responseToModel(response.data)
            return BaseModelIterator.toModel<UserModel>(response.message, model)
        }

        return BaseModelIterator.toModel("El usuario no existe")
    }


}
