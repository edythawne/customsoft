import type { BaseResponse } from '@/data/response/BaseResponse'
import type { UserResponse } from '@/data/response/auth/UserResponse'
import api from '@/data/api'
import type { AxiosResponse } from 'axios'
import type { BasePaginationResponse } from '@/data/response/BasePaginationResponse'
import type { CreateUserRequest } from '@/data/request/app/CreateUserRequest'
import type { CreateUserDetailRequest } from '@/data/request/app/CreateUserDetailRequest'

export default {

    getUserPaginated : function(page: number, search?: string) : Promise<BaseResponse<BasePaginationResponse<UserResponse[]>>> {
        let uri : string = `App/user/index?Page=${page}`

        if (search) {
            uri += `&Search=${search}`
        }

        return new Promise((resolve, reject) => {
            api.get(uri)
                .then((axiosResponse: AxiosResponse) => {
                    const response : BaseResponse<BasePaginationResponse<UserResponse[]>> = axiosResponse.data
                    resolve(response)
                }).catch((error) => reject(error))
        })
    },


    newUser : function(request : CreateUserRequest) : Promise<BaseResponse<number>> {
        return new Promise((resolve, reject) => {
            api.post("App/user/store", request)
                .then((axiosResponse: AxiosResponse) => {
                    const response : BaseResponse<number> = axiosResponse.data
                    resolve(response)
                }).catch((error) => reject(error))
        })
    },

    getUserById : function(id: number) : Promise<BaseResponse<UserResponse>> {
        const uri : string = `App/user/by/id?Id=${id}`

        return new Promise((resolve, reject) => {
            api.get(uri)
                .then((axiosResponse: AxiosResponse) => {
                    const response : BaseResponse<UserResponse> = axiosResponse.data
                    resolve(response)
                }).catch((error) => reject(error))
        })
    },

    syncUserDetail : function(request : CreateUserDetailRequest) : Promise<BaseResponse<number>> {
        return new Promise((resolve, reject) => {
            api.post("App/store/detail", request)
                .then((axiosResponse: AxiosResponse) => {
                    const response : BaseResponse<number> = axiosResponse.data
                    resolve(response)
                }).catch((error) => reject(error))
        })
    },

}
