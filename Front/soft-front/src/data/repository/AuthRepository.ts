import type { BaseResponse } from '@/data/response/BaseResponse'
import type { UserResponse } from '@/data/response/auth/UserResponse'
import api from '@/data/api'
import type { LoginRequest } from '@/data/request/auth/LoginRequest'
import type { AxiosResponse } from 'axios'
import type { BasePaginationResponse } from '@/data/response/BasePaginationResponse'
import type { RoleResponse } from '@/data/response/auth/RoleResponse'

export default {

    login : function(request: LoginRequest) : Promise<BaseResponse<UserResponse>> {
        return new Promise((resolve, reject) => {
            api.post('Auth/login', request)
                .then((axiosResponse: AxiosResponse) => {
                    const response : BaseResponse<UserResponse> = axiosResponse.data
                    resolve(response)
                }).catch((error) => reject(error))
        })
    },

    getAllRole : function() : Promise<BaseResponse<RoleResponse[]>> {
        return new Promise((resolve, reject) => {
            api.get("Role/get/all")
                .then((axiosResponse: AxiosResponse) => {
                    const response : BaseResponse<RoleResponse[]> = axiosResponse.data
                    resolve(response)
                }).catch((error) => reject(error))
        })
    }

}
