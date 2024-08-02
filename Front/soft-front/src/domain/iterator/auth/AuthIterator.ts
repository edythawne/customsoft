import type { LoginModelRequest } from '@/domain/model/auth/request/LoginModelRequest'
import type { LoginRequest } from '@/data/request/auth/LoginRequest'
import EncryptHelper from '@/domain/helper/EncryptHelper'


export default {

    loginModelToRequest(requestModel : LoginModelRequest) : LoginRequest {
        return  {
            email : requestModel.email,
            password : EncryptHelper.toSha256(requestModel.password)
        } as LoginRequest
    },


}
