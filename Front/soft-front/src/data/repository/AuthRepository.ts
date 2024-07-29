import type { BaseResponse } from '@/data/entity/BaseResponse'
import type { UserEntity } from '@/data/entity/Auth/UserEntity'
import api from '@/data/api'
import type { LoginRequest } from '@/data/request/Auth/LoginRequest'
import type { AxiosResponse } from 'axios'

export default {

    login : function(request: LoginRequest) : Promise<BaseResponse<UserEntity>> {
        return new Promise((resolve, reject) => {
            api.post('Auth/login', request)
                .then((axiosResponse: AxiosResponse) => {
                    const response : BaseResponse<UserEntity> = axiosResponse.data
                    resolve(response)
                }).catch((error) => reject(error))
        })
    }

}
